using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackAndForth : MonoBehaviour
{   
    private float _width;
    public float speed = 1.0f;
    private int _direction = 1;
    private EnemyMovement _enemyMovement;
    private Rigidbody2D _body;
    private float _backgroundWidth;
    private float _backgroundPosition;
    private EnemyHealth _enemyHealth;
    public float limit = 0;
    private float start;
    public bool inverted = false;

    // Start is called before the first frame update
    void Start()
    {
        if (inverted) {
            _direction = -1;
        }
        start = transform.position.x;
        _width = GetComponent<SpriteRenderer>().bounds.size.x / 2;
        _backgroundWidth =  GameObject.Find("Background").GetComponent<SpriteRenderer>().bounds.size.x / 2;
        _backgroundPosition = GameObject.Find("Background").transform.position.x;
        _enemyMovement = GetComponent<EnemyMovement>();
        _body = GetComponent<Rigidbody2D>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    void FixedUpdate() {
        if(_enemyMovement.getActivated()) {
            if (!_enemyHealth.dead) {

                //check if direction needs to flip
                if (limit == 0) {
                    if (transform.position.x - _width <= _backgroundPosition - _backgroundWidth) {
                        _direction = 1;
                    } else if (transform.position.x + _width >= _backgroundPosition + _backgroundWidth) {
                        _direction = -1;
                    }
                }
                else {
                    if (transform.position.x - start >= limit && _direction == 1) {
                        _direction = -1;
                    } else if (transform.position.x - start <= -limit && _direction == -1) {
                        _direction = 1;
                    }
                }

                _body.velocity = new Vector2(_direction * speed, _body.velocity.y);
            } else {
                _body.velocity = new Vector2(0, 0);
            }
        }
    }
}
