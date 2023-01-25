using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{

    public static SoundController Instance;

    public AudioSource soundEffect;
    public AudioSource music;
    public AudioSource levelUpSounds;
    public AudioClip[] songs;
    public AudioClip[] skillSoundFX;
    public AudioClip levelUp;
    public AudioClip gameOver;
    public AudioClip levelWon;
    public AudioClip loginRegister;
    public AudioClip buttonClick;

    public Slider musicSlider;
    public Slider fxSlider;

    private float lowPitchRange = 0.9f;
    private float highPitchRange = 1.1f;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            DestroyImmediate(this);
            return;
        }

        Instance = this;
    }

    public void PlaySingle(AudioClip clip)
    {
        if (GameController.Instance.autoBattle)
        {
            return;
        }
        RandomizeSoundEffect(clip);
        soundEffect.Play();
    }

    public void PlayLevelSingle(AudioClip clip)
    {
        if (GameController.Instance.autoBattle)
        {
            return;
        }
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        levelUpSounds.pitch = randomPitch;
        levelUpSounds.clip = clip;

        levelUpSounds.Play();
    }

    public void LevelUp()
    {
        PlayLevelSingle(levelUp);
    }

    public void GameOver()
    {
        PlaySingle(gameOver);
    }

    public void LevelWon()
    {
        PlaySingle(levelWon);
    }

    public void LoginRegister()
    {
        PlaySingle(loginRegister);
    }

    public void ButtonClick()
    {
        PlaySingle(buttonClick);
    }

    public void PlaySingle(int effectNum)
    {
        PlaySingle(skillSoundFX[effectNum]);
        
        /* switch (effectNum)
        {
            case 0:
                PlaySingle(healPotion);
                break;
            case 1:
                PlaySingle(coin1);
                break;
            case 2:
                PlaySingle(coin2);
                break;
            case 3:
                PlaySingle(xBow);
                break;
            case 4:
                PlaySingle(shop);
                break;
            case 5:
                PlaySingle(charDeath);
                break;
            case 6:
                PlaySingle(logIn);
                break;
            case 7:
                PlaySingle(skipStory);
                break;
            case 8:
                PlaySingle(purchase);
                break;
            default:
                break;
        }*/
    }

    public void PlayRandomSingle(params AudioClip[] clips)
    {
        int randomSoundIndex = Random.Range(0, clips.Length);
        soundEffect.clip = clips[randomSoundIndex];
        soundEffect.Play();
    }

    private void RandomizeSoundEffect(AudioClip clip)
    {
        float randomPitch = Random.Range(lowPitchRange, highPitchRange);

        soundEffect.pitch = randomPitch;
        soundEffect.clip = clip;

    }

    public void PlayGameMusic(int levelNum)
    {
        music.clip = songs[levelNum];
        music.Play();
    }


    public void MusicVolume()
    {
        music.volume = musicSlider.value;
    }

    public void FXVolume()
    {
        soundEffect.volume = fxSlider.value;
        levelUpSounds.volume = fxSlider.value;
    }

    public void SetSliders()
    {
        fxSlider.value = soundEffect.volume;
        musicSlider.value = music.volume;
    }
}