using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(EventNames.GameJam_Events.GAME_OVER, this.LoadGameOver);
    }

    public void LoadGameOver(Parameters parameters)
    {
        SceneManager.LoadScene(2);
        EventBroadcaster.Instance.RemoveAllObservers();
        
    }
    public void PlayAgainClicked(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
        EventBroadcaster.Instance.RemoveAllObservers();
    }

    public void ExitToMainMenuClicked(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
        EventBroadcaster.Instance.RemoveAllObservers();
    }
}
