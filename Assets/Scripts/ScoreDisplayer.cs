using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class ScoreDisplayer : MonoBehaviour
{
    public Text creditscoreText;

    void Update()
    {
        creditscoreText.text = "Score: " + Mathf.Round(PlayerScoreStatic.GetPlayerScore());
    }
}
