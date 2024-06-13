using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int initialScore = 0;
    public int currentHealth;
    public int pScore;

    public HealthBar healthBar;
    public Score playerScore;

    public const string DAMAGE_VALUE = "DAMAGE_VALUE";
    public const string ADD_SCORE = "ADD_SCORE";

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        pScore = initialScore;
        healthBar.SetMaxHealth(maxHealth);

        EventBroadcaster.Instance.AddObserver(EventNames.GameJam_Events.ON_DAMAGE, this.OnDamage);
        EventBroadcaster.Instance.AddObserver(EventNames.GameJam_Events.ADD_SCORE, this.AddScore);

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

    void AddScore(Parameters parameters)
    {
        int score = parameters.GetIntExtra(ADD_SCORE, 1);
        pScore += score;
        playerScore.UpdateScore(pScore);

    }
}
