using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour
{
    public string currentScene;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if(Input.GetKeyDown(KeyCode.R))
            SceneManager.LoadScene(currentScene);
        else if(Input.GetKeyDown(KeyCode.Alpha1))
            SceneManager.LoadScene("SampleScene");
        else if(Input.GetKeyDown(KeyCode.Alpha2))
            SceneManager.LoadScene("Level1");
        else if(Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("MainMenu");
        */
        if(Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene("MainMenu");
    }
}
