using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{   
    bool _activated = false;
    private Rigidbody2D _body;
    public float speed = 10.0f;
    private EnemyHealth _enemyHealth;
    private bool velocitySet = false;
    private Vector3 lastDirection;
    public float activationOffset = 0.0f;
    public bool autoActive = true;
    [SerializeField] private GameObject healthBar;
    public bool globalMovement = false;

    // Start is called before the first frame update
    void Start()
    {
        _body = GetComponent<Rigidbody2D>();
        _enemyHealth = GetComponent<EnemyHealth>();
        if (_enemyHealth == null) {
            _enemyHealth = GetComponentInChildren<EnemyHealth>();
        }
        lastDirection = transform.up;
    }

    // Update is called once per frame
    void Update()
    {
        if (!_activated) {
            if (autoActive) {
                Vector3 temporaryPosition = transform.position;
                temporaryPosition.y = temporaryPosition.y + activationOffset;
                Vector3 screenPosition = Camera.main.WorldToViewportPoint(temporaryPosition);

                //check if screen entered, start movement if so
                if (screenPosition.x < 1.1f && screenPosition.x > -0.1f && screenPosition.y < 1.1f && screenPosition.y > -0.1f) {
                    setActivated(true);
                }
            }
        }
    }

    void FixedUpdate() {
        if (_activated  && !_enemyHealth.dead) {
            if (globalMovement) {
                if (!velocitySet) {
                    Vector2 velocity = transform.up * speed;
                    _body.velocity = velocity;
                    velocitySet = true;
                    lastDirection = transform.up;
                }
            }
            else {
                //only set once
                if (!velocitySet || transform.up != lastDirection) {
                    Vector2 velocity = transform.up * speed;
                    _body.velocity = velocity;
                    velocitySet = true;
                    lastDirection = transform.up;
                }
            }
        }
        else {
            //only set it to 0 once
            if(_body.velocity.y != 0) {
                _body.velocity = new Vector3(0, 0, 0);
            }

            if(_enemyHealth.dead) {
                if(healthBar != null)
                    healthBar.SetActive(false);
            }
        }
    }

    public bool getActivated() {
        return _activated;
    }

    public void setActivated(bool activated) {
        if (!_activated) {
            _activated = activated;

            //so these are kind of weird because getcomponentinparent and getcomponents in childred will both return components from this object first
            //check any parents
            if (transform.parent != null) {
                EnemyMovement _parentMovement = transform.parent.GetComponentInParent<EnemyMovement>();
                if (_parentMovement != null) {
                        _parentMovement.setActivated(activated);
                }
            }

            //check any children
            if (transform.childCount != 0) {
                EnemyMovement[] childMovements = GetComponentsInChildren<EnemyMovement>();
                if (childMovements != null) {
                    foreach (EnemyMovement x in childMovements) {
                            x.setActivated(activated);
                    }
                }
            }

            if (healthBar != null) {
                healthBar.SetActive(true);
            }
        }
    }
}
