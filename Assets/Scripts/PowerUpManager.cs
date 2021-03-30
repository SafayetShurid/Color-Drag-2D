using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class PowerUpManager : MonoBehaviour
{

    public GameObject[] powerUps;
    public Transform[] powerUpSpawnPoints;
    public event Action OnRainbowBallActive;
    public event Action OnRainbowBallDeactive;

    public static PowerUpManager instance;
    public Sprite rainbowBallSprite;
    public Sprite defaultBallSprite;

    public bool isRainbowBallActive;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        StartCoroutine(SpawnPowerUpRoutine(PowerUpType.RainbowBall));   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnPowerUp()
    {

    }

    IEnumerator SpawnPowerUpRoutine(PowerUpType powerUpType)
    {
        while(true)
        {
            yield return new WaitForSeconds(Random.Range(5, 10));
            Instantiate(powerUps[0], powerUpSpawnPoints[Random.Range(0,powerUpSpawnPoints.Length)].position, Quaternion.identity);
        }
       
    }

    public void RainbowBallActive()
    {
        OnRainbowBallActive();
        isRainbowBallActive = true;
        Invoke("RainbowBallDeactive", 5f);
    }

    public void RainbowBallDeactive()
    {
        OnRainbowBallDeactive();
        isRainbowBallActive = false;
    }
}
