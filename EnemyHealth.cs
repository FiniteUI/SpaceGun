using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{  
    public float maxHealth = 1.0f;
    private float _health;
    private Animator _animator;
    public bool dead = false;
    private Score score;
    public float killScore = 0;
    public bool takeDamageOnImpact = false;

    // Start is called before the first frame update
    void Start()
    {
        _health = maxHealth;
        _animator = GetComponent<Animator>();
        score = GameObject.FindObjectOfType<Score>();
    }

    public void applyDamage(float damage) {
        _health -= damage;
        if (_health <= 0) {
            //do some animation or something
            _animator.SetBool("dead", true);

            //disable collisions
            dead = true;

            //add score
            score.addScore(killScore);
            
            foreach (Transform child in transform) {
                //kill any children
                if(child.GetComponent<EnemyHealth>() != null) {
                    child.GetComponent<EnemyHealth>().applyDamage(1000);
                }
            }
        }
    }

    public void remove(float delay) {
        foreach (Transform child in transform) {
            if(child.GetComponent<EnemyHealth>() != null) {

            }
            else {
                GameObject.Destroy(child.gameObject);
            }
        }
        Destroy(this.gameObject, delay);
    }

    public float getHealth() {
        return _health;
    }
}
