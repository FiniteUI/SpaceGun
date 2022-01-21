using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private float _health;
    public float maxHealth = 1.0f;
    public bool dead = false;
    private Animator _animator;
    private bool invincible = false;
    public float invincibleTime = 1.0f;
    public float blinkInterval = 0.2f;
    public float blinkOffTime = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        _health = maxHealth;
        _animator = GetComponent<Animator>();
    }

    public void applyDamage(float damage) {
        if (!invincible) {
        _health -= damage;
            if (_health <= 0) {
                //do some animation or something
                dead = true;
                _animator.SetBool("dead", true);
            }
            else {
                //make invinceable for a few frames
                StartCoroutine(iFrames());
            }
        }   
    }

    //may need to add some way to take damage if you're already inside another collider
    void OnTriggerEnter2D(Collider2D other) {
        EnemyHealth health = other.GetComponent<EnemyHealth>();
        if (health != null) {
            if(!health.dead) {
                applyDamage(1.0f);
                if(health.takeDamageOnImpact) {
                    health.applyDamage(1.0f);
                }
            }
        }
    }

    public void remove(float delay) {
        Destroy(this.gameObject, delay);
    }

    public int GetHealth() {
        return (int)_health;
    }

    IEnumerator iFrames() {
        invincible = true;
        float s = 0f;
        Renderer renderer = GetComponent<Renderer>();
        while (s < invincibleTime) {
            s += blinkOffTime;
            renderer.enabled = false;
            yield return new WaitForSeconds(blinkOffTime);
            renderer.enabled = true;
            s += blinkInterval;
            yield return new WaitForSeconds(blinkInterval);
        }
        invincible = false;
    }
}
