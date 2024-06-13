using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    public Text scoreText;

    public void UpdateScore(int scoreCount)
    {
        scoreText.text = "Score: " + Mathf.Round(scoreCount);
    }
}
