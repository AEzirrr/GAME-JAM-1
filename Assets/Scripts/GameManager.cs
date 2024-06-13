using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.GameJam_Events.GAME_OVER, EndGame);

    }

    public void EndGame()
    {
        SceneManager.LoadScene("GameOver");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
