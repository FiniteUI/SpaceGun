using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTurretsWhenDead : MonoBehaviour
{
    private EnemyMovement _enemyMovement;
    private EnemyHealth _enemyHealth;
    private Turret[] _turrets;
    private bool fired = false;

    private void Start() {
        _enemyMovement = GetComponent<EnemyMovement>();
        _enemyHealth = GetComponent<EnemyHealth>();
        _turrets = GetComponentsInChildren<Turret>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!fired) {
            if (_enemyMovement.getActivated()) {
                if (_enemyHealth.dead) {
                    foreach(Turret x in _turrets) {
                        x.fire();
                    }
                    fired = true;
                }
            }
        }
    }
}
