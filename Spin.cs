using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    //degrees per second
    public float spinSpeed = 1;
    private EnemyMovement _enemyMovement;
    private EnemyHealth _enemyHealth;

    private void Start() {
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyHealth = GetComponent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemyMovement.getActivated()) {
            if (!_enemyHealth.dead) {
                Vector3 rotation = new Vector3(0, 0, spinSpeed * Time.deltaTime);
                transform.Rotate(rotation);
            }
        }
    }
}
