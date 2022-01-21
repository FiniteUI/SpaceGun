using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealthBar : MonoBehaviour
{
    private Slider healthSlider;
    [SerializeField] private GameObject boss;
    private EnemyHealth bossHealth;

    // Start is called before the first frame update
    void Start()
    {
        healthSlider = GetComponent<Slider>();
        healthSlider.value = 1;
        bossHealth = boss.GetComponent<EnemyHealth>();
    }//end Start

    private void Update() {
        if (!bossHealth.dead)
            healthSlider.value = bossHealth.getHealth() / bossHealth.maxHealth;
    }
}
