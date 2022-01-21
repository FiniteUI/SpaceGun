using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbit : MonoBehaviour
{
    private Transform _parent;
    public float orbitSpeed = 1.0f;
    public float orbitDistance = 1.0f;
    private EnemyHealth _enemyHealth;
    private EnemyMovement _enemyMovement;
    private Rigidbody2D _rigidBody;
    public float orbitDegrees = 10.0f;
    private bool _orbit = false;
    public bool orbitImmediately = true;
    public bool startAtOrbitDistance = true;
    
    // Start is called before the first frame update
    void Start()
    {
        _parent = transform.parent;
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _rigidBody = GetComponent<Rigidbody2D>();

        if(orbitImmediately) {
            startOrbiting();
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (_parent != null) {
            if (startAtOrbitDistance) {
                Vector3 distance = transform.position - _parent.position;
                if(distance.magnitude >= orbitDistance) {
                    startOrbiting();
                }
            }

            if (_enemyMovement.getActivated()  && !_enemyHealth.dead && _orbit) {
                _parent = transform.parent;
                
                //orbit around the parent object
                //set the orbit distance
                Vector3 newPoint = VectorFunctions.scalePoint(transform.position, _parent.transform.position, orbitDistance);
                
                float clockwiseAngle = 360.0f;
                float counterClockwiseAngle = 360.0f;
                float angle = 0.0f;
                //try and evenly distance self between other orbitals
                //I don't think this accounts for if they are in the exact same position, but that should be rare
                clockwiseAngle = 360.0f;
                counterClockwiseAngle = 360.0f;
                int clockwiseIndex = -1;
                int counterClockwiseIndex = -1;
                
                for (int i = 0; i < _parent.childCount; i++) {
                    Transform child = _parent.GetChild(i);
                    if(child.GetComponent<Orbit>() != null && child != this.transform) {

                        //this will give 
                        angle = VectorFunctions.clockwiseZAngle(transform.localPosition.normalized, child.localPosition.normalized);
                        if (angle < clockwiseAngle) {
                            clockwiseAngle = angle;
                            clockwiseIndex = i;
                        }

                        angle = 360 - angle;
                        if (angle < counterClockwiseAngle) {
                            counterClockwiseAngle = angle;
                            counterClockwiseIndex = i;
                        }
                    }
                }

                //now get new point
                newPoint = VectorFunctions.rotatePoint(newPoint, _parent.transform.position, orbitDegrees);

                //calculate speed modifier, between 1% and 200%
                float speedModifier = clockwiseAngle / counterClockwiseAngle;
                speedModifier = Mathf.Clamp(speedModifier, 0.9f, 1.25f);
                transform.position = Vector3.MoveTowards(transform.position, newPoint, Time.deltaTime * orbitSpeed * speedModifier);
            }
        }
    }

    void startOrbiting() {
        _orbit = true;
    }
}
