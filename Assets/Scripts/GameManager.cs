using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    [Header("Main")]
    public int health = 100;
    public int highScore = 0;
    public int currentScore = 0;
    public bool isGameOver = false;

    [Header("UI Variables")]
    public HealthBar healthBar;
    public GameObject gameOverPanel;
    public Text currentScoreText;
    public Text highesScoreText;

    [Header("Audio Clips Variable")]
    private bool playedOnceHighScore = false;
    private bool playedOnceGameOver = false;
    private bool playedOnceDanger = false;
    private bool playedOnceCaution = false;

    public static GameManager instance; 

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        healthBar.slider.maxValue = health;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        highesScoreText.text = highScore.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {           
            isGameOver = true;
            gameOverPanel.SetActive(true);

            if (!playedOnceGameOver)
            {
                SoundManager.instance.PlayWithOtherSource(SoundManager.instance.gameOverClip);
                playedOnceGameOver = true;
            }

            if (Input.GetKeyDown(KeyCode.Space))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        if(currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            highesScoreText.text = highScore.ToString();

            if(!playedOnceHighScore)
            {
                SoundManager.instance.PlayWithOtherSource(SoundManager.instance.highScoreClip);
                playedOnceHighScore = true;
            }
        }

      
        currentScoreText.text = currentScore.ToString();

    }

    public void ReduceHealth()
    {
        health -= 10;

        if(health==50 && !playedOnceCaution)
        {
            SoundManager.instance.PlayWithOtherSource(SoundManager.instance.cautionClip);
            playedOnceCaution = true;
        }

        if (health == 10 && !playedOnceDanger)
        {
            SoundManager.instance.PlayWithOtherSource(SoundManager.instance.dangerClip);
            playedOnceDanger = true;
        }
        healthBar.ReduceSliderValue(10f);
    }

    public void ScoreUp()
    {
        currentScore += 10;
    }
}
