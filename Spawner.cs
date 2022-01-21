using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    [SerializeField] private GameObject[] _enemies;
    public float interval = 10.0f;
    private bool _ready = false;
    private bool _waiting = false;
    private EnemyMovement _enemyMovement;
    private EnemyHealth _enemyHealth;
    private bool _enabled = true;
    public bool setParent = false;
    public int limit = 0;
    private int unitCount = 0;

    // Start is called before the first frame update
    void Start()
    {
        _enemyMovement = GetComponentInParent<EnemyMovement>();
        _enemyHealth = GetComponentInParent<EnemyHealth>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (_enabled) {
            if (_enemyMovement.getActivated() && !_enemyHealth.dead) {
                if (_ready) {
                    if (limit != 0) {
                        if (unitCount < limit) {
                            spawnUnit();
                        }
                    }
                    else {
                        spawnUnit();
                    }
                } else {
                    if (!_waiting) {
                        StartCoroutine(waitForInterval());
                    }
                }
            }
        }
    }

    IEnumerator waitForInterval() {
        _waiting = true;
        _ready = false;
        yield return new WaitForSeconds(interval); 
        _ready = true;
        _waiting = false;
    }

    void spawnUnit() {
        //spawn unit, wait
        _ready = false;
        int index = Random.Range(0, _enemies.Length);
        GameObject newEnemy = Instantiate(_enemies[index]);
        unitCount++;
        newEnemy.transform.position = transform.position;
        newEnemy.transform.rotation = transform.rotation;

        if(setParent) {
            newEnemy.transform.SetParent(transform.parent);
        }

        StartCoroutine(waitForInterval());
    }

    void setEnabled(bool enable) {
        _enabled = enable;
    }

}
