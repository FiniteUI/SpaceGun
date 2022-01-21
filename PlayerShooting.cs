using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooting : MonoBehaviour
{   
    [SerializeField] private Laser _projectile;
    //public float startup = 0.0f;
    //public float cooldown = 0.0f;
    public float delay = 0.5f;
    private bool _ready = true;
    public Vector2 offset = new Vector2 (0, 0);
    private PlayerMovement _playerMovement;
    private PlayerHealth _playerHealth;
    //private GameObject _boss;
    [SerializeField] private GameObject _boss;

    // Start is called before the first frame update
    void Start()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerHealth = GetComponent<PlayerHealth>();
         //_boss = GameObject.Find("Boss-A");
    }

    // Update is called once per frame
    void Update()
    {
        if (_boss != null) {
            if (!_playerHealth.dead && !_boss.GetComponent<EnemyHealth>().dead) {
                if (Input.GetKey(KeyCode.Space)) {
                    if (_ready) {
                        //Debug.Log("Fire!");
                        Vector3 position = new Vector3(transform.position.x + offset.x, transform.position.y + offset.y, transform.position.z);
                        Laser bullet = Instantiate(_projectile);
                        bullet.setParent(this.gameObject);
                        bullet.transform.position = position;
                        bullet.transform.rotation = transform.rotation;
                        //bullet.setInitialVelocity(_playerMovement.baseMapSpeed);
                        StartCoroutine(fireDelay());
                    }
                }
            }
        }
    }

    IEnumerator fireDelay() {
        _ready = false;
        yield return new WaitForSeconds(delay);
        _ready = true;
    }//end cooldown
}
