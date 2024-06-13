using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public PlayerMovement playerMovement;


    public int maxHealth = 100;
    public int initialScore = 0;
    public int currentHealth;
    public int pScore;

    public HealthBar healthBar;
    public Score playerScore;
    public int scoreMultiplier;

    public const string DAMAGE_VALUE = "DAMAGE_VALUE";
    public const string ADD_SCORE = "ADD_SCORE";

    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth;
        pScore = initialScore;
        healthBar.SetMaxHealth(maxHealth);
        scoreMultiplier = 1;

        EventBroadcaster.Instance.AddObserver(EventNames.GameJam_Events.ON_DAMAGE, this.OnDamage);
        EventBroadcaster.Instance.AddObserver(EventNames.GameJam_Events.ON_MEDKIT, this.OnMedkit);
        EventBroadcaster.Instance.AddObserver(EventNames.GameJam_Events.ON_ADRENALINE, this.OnAdrenaline);
        EventBroadcaster.Instance.AddObserver(EventNames.GameJam_Events.ON_SCORE_MULTIPLIER, this.OnScoreMultiplier);
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

        if (currentHealth <= 0)
        {

            EventBroadcaster.Instance.PostEvent(EventNames.GameJam_Events.GAME_OVER, parameters);
        }
    }

    void OnMedkit(Parameters parameters)
    {
        currentHealth = 100;
        healthBar.SetHealth(currentHealth);
        Debug.Log(currentHealth);
    }

    void OnAdrenaline (Parameters parameters)
    {
        StartCoroutine(playerMovement.Adrenaline(10));
       
    }

    void OnScoreMultiplier(Parameters parameters)
    {
        StartCoroutine(ScoreMultiplier(10));
    }

    void AddScore(Parameters parameters)
    {
        int score = parameters.GetIntExtra(ADD_SCORE, 1);
        
        pScore += score * 100 * scoreMultiplier;

        playerScore.UpdateScore(pScore);

    }

    private IEnumerator ScoreMultiplier(float time)
    {
        scoreMultiplier = 2;
        yield return new WaitForSeconds(time);

        scoreMultiplier = 1;
    }
}
