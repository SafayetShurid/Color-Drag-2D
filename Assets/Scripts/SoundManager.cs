using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{

    public static SoundManager instance;

    [Header("AudioClips")]
    public AudioClip rainbowClip;
    public AudioClip zap;
    public AudioClip[] ballShootClips;    
    public AudioClip highScoreClip;
    public AudioClip gameOverClip;
    public AudioClip getReadyClip;
    public AudioClip dangerClip;
    public AudioClip cautionClip;

    [Header("AudioSources")]
    public AudioSource powerUpSource;
    public AudioSource ballSource;
    public AudioSource enmeyDestroyerAudioSource;  
    public AudioSource otherSource;

    private ColorType previousBallColorType;
    private int ballSoundChangeCounter;

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        Invoke("PlayGetReady", 2f);
    }

    public void PlayGetReady()
    {
        otherSource.PlayOneShot(getReadyClip);
    }

    public void PlayWithEnemyDestroyerSource(AudioClip audioClip)
    {
        enmeyDestroyerAudioSource.PlayOneShot(audioClip);       
    }

    public void PlayWithPowerUpSource(AudioClip audioClip)
    {
        powerUpSource.PlayOneShot(audioClip);

    }

    public void PlayWithBallSource(ColorType colorType)
    {
        if(previousBallColorType.Equals(colorType))
        {
            if(ballSoundChangeCounter<3)
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

    public void PlayWithOtherSource(AudioClip audioClip)
    {
        otherSource.PlayOneShot(audioClip);
    }

    

    
}
