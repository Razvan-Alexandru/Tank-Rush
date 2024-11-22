using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    
    public int score;
    public float maxHealth;
    private float currentHealth;
    public Slider healthBar;
    public Image healthBarFill;
    private bool isPulsing = false;
    public Color normalColour;
    public Color pulseColour;

    public Text timerText;
    public float timer = 0f;

    private float tempTimer = 0f;

    public Text scoreText;

    public Canvas gameUI;
    public GameObject explosionParticle;

    private bool isDead = false;


    void Awake() {
        Time.timeScale = 1.0f;
    }

    void Start() {
        currentHealth = maxHealth;
        score = 0;
        UpdateHealthBar();
        UpdateTimer();
        UpdateScoreText();
    }

    void Update() {
        timer += Time.deltaTime;
        tempTimer += Time.deltaTime;
        AddScoreAfterSeconds(10, 10f);
        UpdateHealthBar();
        UpdateTimer();
        UpdateScoreText();

    }

    void AddScoreAfterSeconds(int score, float time) {
        if(tempTimer >= time) {
            AddScore(score);
            tempTimer = 0f;
        }
    }

    public void AddScore(int amount) {
        score += amount;
    }

    public void TakeDamage(float amount) {
        currentHealth -= amount;
        if(!isDead && currentHealth <= 0) {
            isDead = true;
            StartCoroutine(Die());
        }
    }

    private void UpdateHealthBar() {
        healthBar.value = currentHealth / maxHealth;
        
        if(currentHealth <= maxHealth * 0.2f) {
            if(!isPulsing) {
                isPulsing = true;
                StartCoroutine(PulsingHealthBar());
            }
        }
        else {
            if(isPulsing) {
                isPulsing = false;
                StopCoroutine(PulsingHealthBar());
                healthBarFill.GetComponent<Image>().color = normalColour;
            }
        }
    }

    IEnumerator PulsingHealthBar() {
        while(isPulsing) {
            healthBarFill.GetComponent<Image>().color = Color.Lerp(pulseColour, Color.white, Mathf.PingPong(Time.time * 2f, 1));
            yield return null;
        }
    }

    private void UpdateTimer() {
        int minutes = Mathf.FloorToInt(timer / 60);
        int seconds = Mathf.FloorToInt(timer % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    private void UpdateScoreText() {
        scoreText.text = score.ToString();
    }

    private IEnumerator Die() {
        gameObject.GetComponent<TankMovement>().enabled = false;
        ParticleManager.PlayParticles(explosionParticle, transform.position);
        yield return new WaitForSeconds(1.5f);
        gameUI.GetComponent<UIManager>().isGameOver = true;
    }
}
