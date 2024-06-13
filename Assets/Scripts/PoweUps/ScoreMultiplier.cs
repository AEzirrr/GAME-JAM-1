using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreMultiplier : MonoBehaviour
{
    PowerUPSpawner powerUp;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            powerUp.PowerUpPickedUp(transform.position);
            Parameters parameters = new Parameters();
            EventBroadcaster.Instance.PostEvent(EventNames.GameJam_Events.ON_SCORE_MULTIPLIER, parameters);
            this.gameObject.SetActive(false);
        }
    }
    void Start()
    {
        powerUp = FindObjectOfType<PowerUPSpawner>();
    }


    void Update()
    {
        transform.Rotate(Vector3.up, 30f * Time.deltaTime);
    }
}
