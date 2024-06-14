using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerScoreStatic
{
    public static int Playerscore;

    public static void SetPlayerScore(int score)
    {
        PlayerScoreStatic.Playerscore = score;
        Debug.Log("setplayer score" + PlayerScoreStatic.Playerscore);
    }

    public static int GetPlayerScore()
    {
        Debug.Log("getplayer score" + PlayerScoreStatic.Playerscore);
        return PlayerScoreStatic.Playerscore;
    }
}
