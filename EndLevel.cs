using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class EndLevel : MonoBehaviour
{
    [SerializeField] GameObject player;
    [SerializeField] private GameObject _boss;
    private EnemyHealth _enemyHealth;
    private PlayerHealth _playerHealth;
    [SerializeField] GameObject GameOverUI;
    [SerializeField] GameObject LevelClearedUI;
    [SerializeField] GameObject RetryButton;
    [SerializeField] GameObject NextLevelButton;

    // Start is called before the first frame update
    void Start()
    {
        _enemyHealth = _boss.GetComponent<EnemyHealth>();
        _playerHealth = player.GetComponent<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_enemyHealth.dead || _playerHealth.dead) {
            StartCoroutine(endLevel());
        }
    }

    IEnumerator endLevel() {
        yield return new WaitForSeconds(1);
        Time.timeScale = 0;

        //show endlevel UI
        if (_enemyHealth.dead) {
            LevelClearedUI.SetActive(true);
            EventSystem.current.SetSelectedGameObject(NextLevelButton);
        }
        else {
            GameOverUI.SetActive(true);
            EventSystem.current.SetSelectedGameObject(RetryButton);
        }
    }
}
