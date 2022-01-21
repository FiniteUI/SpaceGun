using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    
    [SerializeField] private GameObject _projectile;
    public int burstSize = 1;
    public float burstDelay = 2.0f;
    public float shotDelay = 0.3f;
    public bool aim = false;
    public float aimingFireDelay = 1.0f;
    public Vector3 projectileOffset;
    public float aimTime = 1.0f;
    public float totalCooldownTime = 1.0f;
    public bool child = true;
    public float shotSpeed = 300.0f;
    public bool autoTurret = true;
    public int projectileCount = 1;
    public float spread = 0.0f;
    public float firingAngle = 0.0f;

    private bool _ready = true;
    private bool _aiming = false;
    private bool _aimDelay = false;
    private bool _aimed = false;
    private bool _burstCooldown = false;
    private bool _aimingDone = false;
    private EnemyMovement _enemyMovement;
    private EnemyHealth _enemyHealth;
    private GameObject _player;
    private int _shotCount = 0;
    private bool _totalCoolDown = false;

    // Start is called before the first frame update
    void Start()
    {
        _enemyMovement = GetComponentInParent<EnemyMovement>();
        _enemyHealth = GetComponentInParent<EnemyHealth>();
        _player = GameObject.Find("PlayerShip");
    }

    // Update is called once per frame
    void Update()
    {   
        if(autoTurret) {
            if (_enemyMovement.getActivated()) {
                if (!_enemyHealth.dead) {
                    if (aim) {
                        if (_player != null) {
                            if(!_totalCoolDown) {

                                //start aiming
                                if(_aimed) {
                                    if(!_burstCooldown) {
                                        if(_ready) {
                                            fire();
                                        }
                                    }
                                } else {
                                    if(!_aimDelay) {
                                        if (!_aimingDone) {
                                            if (!_aiming) {
                                                StartCoroutine(waitForAiming());
                                            }
                                            aimTurret();
                                        } else {
                                            StartCoroutine(waitForAimingFireDelay());
                                        }
                                    } 
                                }
                            }
                        }
                    } else {
                        //if not aiming
                        if(!_totalCoolDown) {
                            if(!_burstCooldown) {
                                if(_ready) {
                                    fire();
                                }
                            }
                        }
                    } 
                }
            }
        }
    }

    void aimTurret() {
        Vector3 player = _player.transform.position;
        if (player != null) {
            float angle = VectorFunctions.calculateZAngleToPoint(transform.position, player, transform.up);
            if (angle != 0) {
                Quaternion rotation = transform.rotation * Quaternion.AngleAxis(angle, Vector3.forward);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation, 0.1f); 
            }
        }
    }

    IEnumerator waitForAimingFireDelay() {
        _aimDelay = true;
        yield return new WaitForSeconds(aimingFireDelay);
        _aimDelay = false;
        _aimed = true;
    }

    IEnumerator waitForTotalCooldown() {
        _totalCoolDown = true;
        yield return new WaitForSeconds(totalCooldownTime);
        _totalCoolDown = false;
    }

    IEnumerator waitForAiming() {
        _aiming = true;
        yield return new WaitForSeconds(aimTime);
        _aiming = false;
        _aimingDone = true;
    }
    
    public void fire() {
        createProjectile();
        StartCoroutine(waitForShotDelay());
        _shotCount+=1;
        if (_shotCount >= burstSize) {
            _aimed = false;
            _aimingDone = false;
            _shotCount = 0;
            StartCoroutine(waitForBurstDelay());
            StartCoroutine(waitForTotalCooldown());
        }
    }

    IEnumerator waitForBurstDelay() {
        _burstCooldown = true;
        yield return new WaitForSeconds(burstDelay);
        _burstCooldown = false;
    }

    IEnumerator waitForShotDelay() {
        _ready = false;
        yield return new WaitForSeconds(shotDelay);
        _ready = true;
    }

    void createProjectile() {
        float spreadInterval;
        if (projectileCount > 1)
            spreadInterval = spread / (projectileCount - 1);
        else
            spreadInterval = 0;

        float lastAngle =  firingAngle - spread / 2;
        for (int i = 0; i < projectileCount; i++) {
            GameObject newProjectile = Instantiate(_projectile);
            Laser _laser = newProjectile.GetComponent<Laser>();
            if (child) {
                _laser.setParent(this.gameObject.transform.parent.gameObject);
            }
            _laser.setSpeed(shotSpeed);

            _laser.setEnemyLaser();
            newProjectile.transform.position = transform.position + transform.TransformDirection(projectileOffset);
            newProjectile.transform.rotation = transform.rotation;

            _laser.transform.Rotate(new Vector3(0, 0, lastAngle));
            lastAngle = lastAngle + spreadInterval;
        }  
    }
}
