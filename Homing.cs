using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Homing : MonoBehaviour
{
    public float homingSpeed = 1.0f;
    [SerializeField] private GameObject target;
    private Transform targetPosition;
    private EnemyMovement _enemyMovement;
    private EnemyHealth _enemyHealth;

    // Start is called before the first frame update
    void Start()
    {
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemyMovement.getActivated()) {
            if (!_enemyHealth.dead) {
                //calculate the angle between current direction and direction to target
                targetPosition = target.GetComponent<Transform>();
                
                float angle = VectorFunctions.calculateZAngleToPoint(transform.position, targetPosition.position, transform.up);
                angle = angle * homingSpeed * Time.deltaTime;

                //update our angle accordingly
                if (angle != 0) {
                    transform.Rotate(new Vector3(0, 0, angle));
                }
            }
        }
    }

    public void setTarget(GameObject target) {
        this.target = target;
    }
}
