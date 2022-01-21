using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    public float laserSpeed = 25.0f;
    //private float _initialVelocity = 0.0f;
    public float damage = 1.0f;
    private GameObject _parent;
    private bool _playerLaser = true;

    //maybe do this using velocity instead of translate?

    // Update is called once per frame
    void Update()
    {
        //delete if off the screen
        //could replace this with delete when off screen script
        /*
        Vector3 currentPosition = Camera.main.WorldToViewportPoint(transform.position);
        if (currentPosition.x < 0 || currentPosition.x > 1 || currentPosition.y < 0 || currentPosition.y > 1) {
            Destroy(this.gameObject);
        }
        */

        transform.Translate(0, (laserSpeed) * Time.deltaTime, 0);
        //transform.Translate(0, (laserSpeed + _initialVelocity) * Time.deltaTime, 0);
    }

    public void setEnemyLaser() {
        _playerLaser = false;
    }

    /*
    void FixedUpdate() {
        //move the projectile forward
        //transform.Translate(0, (laserSpeed + _initialVelocity) * Time.deltaTime, 0);
        transform.Translate(0, (laserSpeed) * Time.deltaTime, 0);
    }

    public void setInitialVelocity(float velocity) {
        _initialVelocity = velocity;
    }
    */

    public void setSpeed(float speed) {
        laserSpeed = speed;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject != _parent) {

            if (_playerLaser) {
                EnemyHealth health = other.GetComponent<EnemyHealth>();
                if (health != null) {
                    if (!health.dead) {
                        health.applyDamage(damage);
                        Destroy(this.gameObject);
                    }
                }
            } else {
                PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
                if (playerHealth != null) {
                    if (!playerHealth.dead) {
                        playerHealth.applyDamage(damage);
                        Destroy(this.gameObject);
                    }
                }
            }
        }
    }

    public void setParent(GameObject parent) {
        _parent = parent;
    }
}
