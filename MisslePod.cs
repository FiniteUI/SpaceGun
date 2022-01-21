using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisslePod : MonoBehaviour
{
    [SerializeField] private GameObject missle;
    [SerializeField] private GameObject homingMissle;
    private GameObject currentMissle;

    //instantiate the missle object in the world
    public void loadMissle() {
        if (currentMissle == null) {
            currentMissle = Instantiate(missle, transform.position, transform.rotation);
            currentMissle.transform.SetParent(transform);
            currentMissle.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }
    }

    public void loadMissle(GameObject target) {
        if (currentMissle == null) {
            currentMissle = Instantiate(homingMissle, transform.position, transform.rotation);
            currentMissle.GetComponent<Homing>().setTarget(target);
            currentMissle.transform.SetParent(transform);
            currentMissle.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        }
    }

    public void fireMissle() {
        if (currentMissle != null) {
            EnemyMovement missleMovement = currentMissle.GetComponent<EnemyMovement>();
            missleMovement.transform.SetParent(null);
            currentMissle.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            missleMovement.setActivated(true);
            currentMissle = null;
        }
    }
}
