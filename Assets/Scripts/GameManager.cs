using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public int health = 100;
    public int highScore = 0;
    public int currentScore = 0;
    public HealthBar healthBar;
    public GameObject gameOverPanel;

  
    public Text currentScoreText;

    public static GameManager instance; 

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        healthBar.slider.maxValue = health;
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Debug.Log("Game Over");
            gameOverPanel.SetActive(true);
        }

        if(currentScore > highScore)
        {
            highScore = currentScore;
            PlayerPrefs.SetInt("HighScore", highScore);
        }

      
        currentScoreText.text = currentScore.ToString();

    }

    public void ReduceHealth()
    {
        health -= 10;
        healthBar.ReduceSliderValue(10f);
    }

    public void ScoreUp()
    {
        currentScore += 10;
    }
}
