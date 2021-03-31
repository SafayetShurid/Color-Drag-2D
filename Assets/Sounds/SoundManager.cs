using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

    public AudioClip rainbowClip;
    public AudioClip zap;
    public AudioClip[] ballShootClips;
    public AudioSource powerUpSource;
    public AudioSource ballSource;
    public AudioSource enmeyDestroyerAudioSource;

    private ColorType previousBallColorType;
    private int ballSoundChangeCounter;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayWithEnemyDestroyerSource(AudioClip audioClip)
    {
        enmeyDestroyerAudioSource.PlayOneShot(audioClip);
        Debug.Log("Sound");
    }

    public void PlayWithPowerUpSource(AudioClip audioClip)
    {
        powerUpSource.PlayOneShot(audioClip);

    }

    public void PlayWithBallSource(ColorType colorType)
    {
        if(previousBallColorType.Equals(colorType))
        {
            if(ballSoundChangeCounter<4)
            {
                ballSoundChangeCounter++;
            }
          
        }
        else
        {
            ballSoundChangeCounter = 0;
        }
        previousBallColorType = colorType;
        ballSource.PlayOneShot(ballShootClips[ballSoundChangeCounter]);
    }
}
