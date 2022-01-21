using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelUIButtons : MonoBehaviour
{
    [SerializeField] string currentScene;
    [SerializeField] string nextLevel;

    public void RetryClicked() {
        SceneManager.LoadScene(currentScene);
    }

    public void ExitClicked() {
        SceneManager.LoadScene("MainMenu");
    }

    public void NextLevelClicked() {
        SceneManager.LoadScene(nextLevel);
    }
}
