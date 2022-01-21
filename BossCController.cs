using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossCController : MonoBehaviour
{   

    //build an attack encoding system
    private EnemyMovement _enemyMovement;
    private EnemyHealth _enemyHealth;
    private MisslePod[] _missilePods;
    private List<string> _attacks;
    private Turret _turret;
    private bool attacking = false;
    private GameObject _player;
    public Vector2 cooldownRange;
    public bool accountForVelocityWhenAiming = false;

    // Start is called before the first frame update
    void Start()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _missilePods = GetComponentsInChildren<MisslePod>();
        _turret = GetComponentInChildren<Turret>();
        _player = GameObject.Find("PlayerShip");
        loadAttacks();
    }

    // Update is called once per frame
    void Update()
    {   
        if (_enemyMovement.getActivated()) {
            if (!_enemyHealth.dead) {
                if (_player != null) {
                    if(!_player.GetComponent<PlayerHealth>().dead) {
                        if (!attacking) {
                            int x = Random.Range(0, _attacks.Count);
                            StartCoroutine(performAttack(_attacks[x]));
                        }
                    }
                } 
            }
        } 
    }

    IEnumerator wait(float seconds) {
        yield return new WaitForSeconds(seconds);
    }

    void fireLasers() {
        _turret.fire();
    }

    IEnumerator performAttack(string attack) {
        int projectileCount = _turret.projectileCount;
        float firingAngle = _turret.firingAngle;
        float spread = _turret.spread;

        //Debug.Log("Attacking!");
        //Debug.Log(attack);
        attacking = true;
        string[] actions = attack.Split(",");
        foreach(string action in actions) {
            //Debug.Log(action);
            string[] actionParts = action.Split("::");
            switch(actionParts[0]) {
                case("LOAD_MISSILE"):
                    if(_missilePods[int.Parse(actionParts[1])] != null)
                        _missilePods[int.Parse(actionParts[1])].loadMissle();
                    break;
                case("LOAD_HOMING_MISSILE"):
                    if(_missilePods[int.Parse(actionParts[1])] != null)
                        _missilePods[int.Parse(actionParts[1])].loadMissle(_player);
                    break;
                case("FIRE_MISSILE"):
                    if(_missilePods[int.Parse(actionParts[1])] != null)
                        _missilePods[int.Parse(actionParts[1])].fireMissle();
                    break;
                case("FIRE_LASERS"):
                    fireLasers();
                    break;
                case("WAIT"):
                    yield return new WaitForSeconds(float.Parse(actionParts[1]));
                    break;
                case("CHANGE_LASER_COUNT"):
                    if(_turret != null)
                        _turret.projectileCount = int.Parse(actionParts[1]);
                    break;
                case("CHANGE_LASER_FIRING_ANGLE"):
                    if(_turret != null)
                        _turret.firingAngle = float.Parse(actionParts[1]);
                    break;
                case("CHANGE_LASER_SPREAD"):
                    if(_turret != null)
                        _turret.spread = float.Parse(actionParts[1]);
                    break;
                case("AIM_LASER"):
                    float angle = 0.0f;
                    if (!accountForVelocityWhenAiming) {
                        angle = VectorFunctions.calculateZAngleToPoint(transform.position, _player.transform.position, transform.up);
                    }
                    else {
                        //need to get the velocity of the player (or maybe just the up velocity)
                        //and the velocity of the projectile
                        //calculate the angle to intersect
                        //get the speed of the laser
                        float laserSpeed = _turret.shotSpeed;

                        //get player speed
                        float playerSpeed = _player.GetComponent<PlayerMovement>().baseMapSpeed;

                        //only position is changing, not distance, so we can use that to get the time to travel from boss to player
                        float distance = Vector2.Distance(_turret.transform.position, _player.transform.position);
                        float time = distance / laserSpeed;

                        //now find out where the player will be at that time
                        float playerPositionY = _player.transform.position.y + (time * playerSpeed);
                        Vector2 playerPosition = _player.transform.position;
                        playerPosition.y = playerPositionY;

                        //now calculate angle to that position
                        angle = VectorFunctions.calculateZAngleToPoint(transform.position, playerPosition, transform.up);
                    }
                    
                    Debug.Log(angle);
                    if(_turret != null)
                        _turret.firingAngle = angle;
                    break;
            }
        }
        float s = Random.Range(cooldownRange.x, cooldownRange.y);
        yield return new WaitForSeconds(s);

        //reset settings
        if(_turret != null) {
            _turret.projectileCount = projectileCount;
            _turret.firingAngle = firingAngle;
            _turret.spread = spread;
        }

        attacking = false;
    }

    void loadAttacks() {
        //format - TYPE::VALUE,
        _attacks = new List<string>();
        _attacks.Add("LOAD_MISSILE::0,WAIT::0.2,LOAD_MISSILE::1,WAIT::0.2,LOAD_MISSILE::2,WAIT::0.2,LOAD_MISSILE::3,WAIT::0.2,LOAD_MISSILE::4,WAIT::0.2,LOAD_MISSILE::5,WAIT::0.5,FIRE_MISSILE::0,FIRE_MISSILE::1,FIRE_MISSILE::2,FIRE_MISSILE::3,FIRE_MISSILE::4,FIRE_MISSILE::5");
        _attacks.Add("LOAD_MISSILE::0,WAIT::0.2,LOAD_MISSILE::5,WAIT::0.2,LOAD_MISSILE::1,WAIT::0.2,LOAD_MISSILE::4,WAIT::0.2,LOAD_MISSILE::2,WAIT::0.2,LOAD_MISSILE::3,WAIT::0.5,FIRE_MISSILE::0,WAIT::0.2,FIRE_MISSILE::5,WAIT::0.2,FIRE_MISSILE::1,WAIT::0.2,FIRE_MISSILE::4,WAIT::0.2,FIRE_MISSILE::2,WAIT::0.2,FIRE_MISSILE::3");
        _attacks.Add("LOAD_HOMING_MISSILE::2,LOAD_HOMING_MISSILE::3,WAIT::1,FIRE_MISSILE::2,FIRE_MISSILE::3");
        //_attacks.Add("FIRE_LASERS,WAIT::0.3,FIRE_LASERS,WAIT::0.3,FIRE_LASERS");
        _attacks.Add("CHANGE_LASER_COUNT::5,FIRE_LASERS,WAIT::0.3,CHANGE_LASER_COUNT::6,FIRE_LASERS,WAIT::0.3,CHANGE_LASER_COUNT::5,FIRE_LASERS");
        //_attacks.Add("CHANGE_LASER_SPREAD::0,CHANGE_LASER_COUNT::1,CHANGE_LASER_FIRING_ANGLE::75,FIRE_LASERS,WAIT::0.1,CHANGE_LASER_FIRING_ANGLE::60,FIRE_LASERS,WAIT::0.1,CHANGE_LASER_FIRING_ANGLE::45,FIRE_LASERS,WAIT::0.1,CHANGE_LASER_FIRING_ANGLE::30,FIRE_LASERS,WAIT::0.1,CHANGE_LASER_FIRING_ANGLE::15,FIRE_LASERS,WAIT::0.1,CHANGE_LASER_FIRING_ANGLE::0,FIRE_LASERS,WAIT::0.1,CHANGE_LASER_FIRING_ANGLE::-15,FIRE_LASERS,WAIT::0.1,CHANGE_LASER_FIRING_ANGLE::-30,FIRE_LASERS,WAIT::0.1,CHANGE_LASER_FIRING_ANGLE::-45,FIRE_LASERS,WAIT::0.1,CHANGE_LASER_FIRING_ANGLE::-60,FIRE_LASERS,WAIT::0.1,CHANGE_LASER_FIRING_ANGLE::-75,FIRE_LASERS,WAIT::0.1");
        //_attacks.Add("AIM_LASER,FIRE_LASERS,WAIT::0.3,FIRE_LASERS,WAIT::0.3,FIRE_LASERS");
        //_attacks.Add("CHANGE_LASER_SPREAD::0,CHANGE_LASER_COUNT::1,AIM_LASER,FIRE_LASERS,WAIT::0.1,FIRE_LASERS,WAIT::0.1,FIRE_LASERS,WAIT::0.1,FIRE_LASERS,WAIT::0.1,FIRE_LASERS,WAIT::0.1,FIRE_LASERS,WAIT::0.1,FIRE_LASERS,WAIT::0.1,FIRE_LASERS,WAIT::0.1,FIRE_LASERS");
        //_attacks.Add("LOAD_MISSILE::0,LOAD_MISSILE::1,LOAD_MISSILE::2,LOAD_MISSILE::3,LOAD_MISSILE::4,LOAD_MISSILE::5,WAIT::0.1,FIRE_MISSILE::0,FIRE_MISSILE::1,FIRE_MISSILE::2,FIRE_MISSILE::3,FIRE_MISSILE::4,FIRE_MISSILE::5,WAIT::0.1,LOAD_MISSILE::0,LOAD_MISSILE::1,LOAD_MISSILE::2,LOAD_MISSILE::3,LOAD_MISSILE::4,LOAD_MISSILE::5,WAIT::0.1,FIRE_MISSILE::0,FIRE_MISSILE::1,FIRE_MISSILE::2,FIRE_MISSILE::3,FIRE_MISSILE::4,FIRE_MISSILE::5,WAIT::0.1,LOAD_MISSILE::0,LOAD_MISSILE::1,LOAD_MISSILE::2,LOAD_MISSILE::3,LOAD_MISSILE::4,LOAD_MISSILE::5,WAIT::0.1,FIRE_MISSILE::0,FIRE_MISSILE::1,FIRE_MISSILE::2,FIRE_MISSILE::3,FIRE_MISSILE::4,FIRE_MISSILE::5,WAIT::0.1,LOAD_MISSILE::0,LOAD_MISSILE::1,LOAD_MISSILE::2,LOAD_MISSILE::3,LOAD_MISSILE::4,LOAD_MISSILE::5,WAIT::0.1,FIRE_MISSILE::0,FIRE_MISSILE::1,FIRE_MISSILE::2,FIRE_MISSILE::3,FIRE_MISSILE::4,FIRE_MISSILE::5,WAIT::0.1,LOAD_MISSILE::0,LOAD_MISSILE::1,LOAD_MISSILE::2,LOAD_MISSILE::3,LOAD_MISSILE::4,LOAD_MISSILE::5,WAIT::0.1,FIRE_MISSILE::0,FIRE_MISSILE::1,FIRE_MISSILE::2,FIRE_MISSILE::3,FIRE_MISSILE::4,FIRE_MISSILE::5");
        _attacks.Add("CHANGE_LASER_SPREAD::0,CHANGE_LASER_COUNT::1,AIM_LASER,FIRE_LASERS,WAIT::0.1,AIM_LASER,FIRE_LASERS,WAIT::0.1,AIM_LASER,FIRE_LASERS,WAIT::0.1,AIM_LASER,FIRE_LASERS,WAIT::0.1,AIM_LASER,FIRE_LASERS,WAIT::0.1,AIM_LASER,FIRE_LASERS,WAIT::0.1,AIM_LASER,FIRE_LASERS,WAIT::0.1,AIM_LASER,FIRE_LASERS,WAIT::0.1,AIM_LASER,FIRE_LASERS");

    }
}
