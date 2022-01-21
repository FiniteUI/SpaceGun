using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Score : MonoBehaviour
{

    private float score = 0;
    private TextMeshProUGUI scoreText;

    private void Start() {
        scoreText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        string text = "SCORE: " + score;
        scoreText.text = text;

    }

    public void addScore(float score) {
        this.score += score;
    }
}
