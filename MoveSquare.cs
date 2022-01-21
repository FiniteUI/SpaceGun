using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum direction {
    POSITIVE_X,
    NEGATIVE_Y,
    NEGATIVE_X,
    POSITIVE_Y
}

public class MoveSquare : MonoBehaviour
{
    private EnemyMovement _enemyMovement;
    private Rigidbody2D _body;
    private EnemyHealth _enemyHealth;
    public float length = 1.0f;
    public float speed = 1.0f;
    private Vector2 lastPoint;
    public direction startingDirection = direction.POSITIVE_X;
    private direction currentDirection;
    private bool directionChange = true;
    public float advanceLength = 1.0f;
    private float currentLength;
    public bool inverted = false;

    // Start is called before the first frame update
    void Start()
    {
        _enemyMovement = transform.parent.GetComponent<EnemyMovement>();
        //_enemyMovement = GetComponent<EnemyMovement>();
        _body = GetComponent<Rigidbody2D>();
        _enemyHealth = GetComponent<EnemyHealth>();
        lastPoint = transform.position;
        currentDirection = startingDirection;
    }

    void FixedUpdate()
    {   
        if(_enemyMovement.getActivated()) {
            if (!_enemyHealth.dead) {
                if (directionChange) {
                    //Vector2 newPoint = Vector2.zero;
                    //Vector2 force = Vector2.zero;
                    switch(currentDirection) {
                        case(direction.POSITIVE_X):
                            //force = new Vector2(speed, 0) - _body.velocity;
                            //_body.velocity = new Vector2(speed, baseVelocity.y);
                            _body.velocity = new Vector2(speed, 0);
                            //newPoint = new Vector2(transform.position.x + length, transform.position.y);
                            break;
                        case(direction.NEGATIVE_Y):
                            //_body.velocity = new Vector2(baseVelocity.x, baseVelocity.y - speed);
                            _body.velocity = new Vector2(0, -speed);
                            //force = new Vector2(0, -speed) - _body.velocity;
                            //newPoint = new Vector2(transform.position.x, transform.position.y - length);
                            break;
                        case(direction.NEGATIVE_X):
                            //_body.velocity = new Vector2(-1 * speed, baseVelocity.y);
                            _body.velocity = new Vector2(-speed, 0);
                            //force = new Vector2(-speed, 0) - _body.velocity;
                            //newPoint = new Vector2(transform.position.x - length, transform.position.y);
                            break;
                        case(direction.POSITIVE_Y):
                            //_body.velocity = new Vector2(baseVelocity.x, baseVelocity.y + speed);
                            //newPoint = new Vector2(transform.position.x, transform.position.y + length);
                            _body.velocity = new Vector2(0, speed);
                            //force = new Vector2(0, speed) - _body.velocity;
                            break;
                    }
                    //_body.AddForce(force);
                    //transform.position = Vector3.MoveTowards(transform.position, newPoint, Time.fixedDeltaTime * speed);
                }
                directionChange = false;
                
                if (currentDirection == direction.NEGATIVE_Y) {
                    currentLength = length + advanceLength;
                }
                else {
                    currentLength = length;
                }

                //change directions if we've gone far enough
                if (Vector2.Distance(lastPoint, transform.position) >= currentLength) {
                    if (!inverted) {
                        currentDirection = (direction)(((int)currentDirection + 1) % System.Enum.GetNames(typeof(direction)).Length);
                    } 
                    else {
                        currentDirection = (direction)(((int)currentDirection - 1) % System.Enum.GetNames(typeof(direction)).Length);
                        if ((int)currentDirection == -1) {
                            currentDirection = direction.POSITIVE_Y;
                        }
                    }
                    lastPoint = transform.position;
                    directionChange = true;
                }  
                else {
                    directionChange = false;
                }
            }
            else {
                _body.velocity = Vector2.zero;
            }
        }
    }
}
