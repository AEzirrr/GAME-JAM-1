using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Medkit : MonoBehaviour
{
    PowerUPSpawner powerUp;
    void Start()
    {
     powerUp = FindObjectOfType<PowerUPSpawner>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            powerUp.PowerUpPickedUp(transform.position);
            Parameters parameters = new Parameters();
            EventBroadcaster.Instance.PostEvent(EventNames.GameJam_Events.ON_MEDKIT, parameters);
            this.gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, 30f * Time.deltaTime);
    }
}
