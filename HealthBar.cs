using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private PlayerHealth _playerHealth;
    [SerializeField] private GameObject[] healthContainers;

    // Start is called before the first frame update
    void Start()
    {
        _playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i < healthContainers.Length; i++) {
            if (i < _playerHealth.maxHealth) {
                healthContainers[i].SetActive(true);

                if (i < _playerHealth.GetHealth()) {
                    healthContainers[i].transform.GetChild(0).gameObject.SetActive(true);
                }
                else {
                    healthContainers[i].transform.GetChild(0).gameObject.SetActive(false);
                }
            }
            else {
                healthContainers[i].SetActive(false);
            }
        }
    }
}
