using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GameManager : MonoBehaviour
{

    public int health = 100;
    public HealthBar healthBar;
    public GameObject gameOverPanel;

    public static GameManager instance; 

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        healthBar.slider.maxValue = health;
    }

    // Update is called once per frame
    void Update()
    {
        if(health <= 0)
        {
            Debug.Log("Game Over");
            gameOverPanel.SetActive(true);
        }
    }

    public void ReduceHealth()
    {
        health -= 10;
        healthBar.ReduceSliderValue(10f);
    }
}
