using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public HealthBar healthBar;

    public const string DAMAGE_VALUE = "DAMAGE_VALUE";

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);

        EventBroadcaster.Instance.AddObserver(EventNames.GameJam_Events.ON_DAMAGE, this.OnDamage);

    }

    // Update is called once per frame
    void Update()
    {
      
    }

    void OnDamage(Parameters parameters) { 

        int damage = parameters.GetIntExtra(DAMAGE_VALUE, 1);

        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }
}
