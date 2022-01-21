using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float verticalMovementSpeed = 350.0f;
    public float hortizontalMovementSpeed = 350.0f;
    public float baseMapSpeed = 200.0f;
    private Rigidbody2D _body;
    private float deltaX = 0.0f;
    private float deltaY = 0.0f;
    public float deadZone = 0.01f;
    private float _objectWidth;
    private float _objectHeight;
    private float _backgroundWidth;
    private float _backgroundPosition;
    private PlayerHealth _playerHealth;
    [SerializeField] private GameObject _boss;

    void Start()
    {
        //grab the rigid body to use for movement
        _body = GetComponent<Rigidbody2D>();
        _objectWidth = GetComponent<SpriteRenderer>().bounds.size.x / 2.0f;
        _objectHeight = GetComponent<SpriteRenderer>().bounds.size.y / 2.0f;
        _playerHealth = GetComponent<PlayerHealth>();
        _backgroundWidth =  GameObject.Find("Background").GetComponent<SpriteRenderer>().bounds.size.x / 2;
        _backgroundPosition = GameObject.Find("Background").transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {   
        if (_boss != null) {
            if (!_playerHealth.dead && !_boss.GetComponent<EnemyHealth>().dead) {
                float liveZone = 1 - deadZone;
                Vector3 minimum = Camera.main.ViewportToWorldPoint(new Vector2(deadZone, deadZone));
                minimum.x = Mathf.Max(minimum.x + _objectWidth, (_backgroundPosition - _backgroundWidth) * liveZone + _objectWidth);
                minimum.y = minimum.y + _objectHeight;

                Vector3 maximum = Camera.main.ViewportToWorldPoint(new Vector2(1, 1) * liveZone) ;
                maximum.x = Mathf.Min(maximum.x - _objectWidth, (_backgroundPosition + _backgroundWidth) * liveZone - _objectWidth);
                maximum.y = maximum.y - _objectHeight;

                //deltaX = Input.GetAxis("Horizontal") * hortizontalMovementSpeed;
                deltaX = Input.GetAxisRaw("Horizontal") * hortizontalMovementSpeed;

                //if they hit the deadzone, don't let them go any farther
                if (deltaX < 0) {
                    if (transform.position.x <= minimum.x) {
                        deltaX = 0;
                    }
                } else if (deltaX > 0) {
                    if (transform.position.x >= maximum.x) {
                        deltaX = 0;
                    }
                }

                //deltaY = Input.GetAxis("Vertical") * verticalMovementSpeed;
                deltaY = Input.GetAxisRaw("Vertical") * verticalMovementSpeed;
                //if they hit the deadzone, don't let them go any farther
                if (deltaY < 0) {
                    if (transform.position.y <= minimum.y) {
                        deltaY = 0;
                    }
                } else if (deltaY > 0) {
                    if (transform.position.y >= maximum.y) {
                        deltaY = 0;
                    }
                }
            }
        }
    }

    //check and perform movement
    void movement() {
        //since this is a vertical shooter, we are always moving forward.
        //the player can move forward faster, backwards, or side to side
        Vector2 movementVelocity;
        movementVelocity = Vector2.zero;
        if (_boss != null) {
            if (!_playerHealth.dead && !_boss.GetComponent<EnemyHealth>().dead) {
                movementVelocity = new Vector2(deltaX, baseMapSpeed + deltaY);
            }
        }
        
		_body.velocity = movementVelocity;
    }

    void FixedUpdate() {
        movement();
    }
}
