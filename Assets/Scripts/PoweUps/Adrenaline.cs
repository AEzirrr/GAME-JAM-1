using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adrenaline : MonoBehaviour
{

    PowerUPSpawner powerUp;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            powerUp.PowerUpPickedUp(transform.position);
            Parameters parameters = new Parameters();
            EventBroadcaster.Instance.PostEvent(EventNames.GameJam_Events.ON_ADRENALINE, parameters);
            this.gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        powerUp = FindObjectOfType<PowerUPSpawner>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, 30f * Time.deltaTime);
    }
}
