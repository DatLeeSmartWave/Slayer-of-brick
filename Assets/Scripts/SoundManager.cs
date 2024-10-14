using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour {
    [SerializeField] private AudioSource soundSource;
    [SerializeField] private Image soundButton;
    [SerializeField] private Sprite soundOn, soundOff;
    [SerializeField] private AudioClip clickSound;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private AudioClip loseSound;
    [SerializeField] private AudioClip lightningSound;
    [SerializeField] private AudioClip explosionSound;
    [SerializeField] private AudioClip ballSound;
    int soundId;

    // Start is called before the first frame update
    void Start() {
        soundId = PlayerPrefs.GetInt(StringManager.soundId, 1);
        PlayerPrefs.SetInt(StringManager.soundId, soundId);
        if (SceneManager.GetActiveScene().name == "HomeScene") {
            
            InitButtonStatus();
        }
        if (PlayerPrefs.GetInt(StringManager.soundId) == 1)
            soundSource.volume = 1;
        else
            soundSource.volume = 0;
    }

    void InitButtonStatus() {
        if (PlayerPrefs.GetInt(StringManager.soundId) == 1)
            soundButton.sprite = soundOn;
        else
            soundButton.sprite = soundOff;
    }

    public void SwitchButton() {
        if (soundButton.sprite == soundOn) {
            soundButton.sprite = soundOff;
            PlayerPrefs.SetInt(StringManager.soundId, 0);
            soundSource.volume = 0;
        } else {
            soundButton.sprite = soundOn;
            PlayerPrefs.SetInt(StringManager.soundId, 1);
            soundSource.volume = 1;
        }
    }

    public void PlayClickSound() {
        soundSource.PlayOneShot(clickSound);
    }

    public void PlayWinSound() {
        soundSource.PlayOneShot(winSound);
    }

    public void PlayLoseSound() {
        soundSource.PlayOneShot(loseSound);
    }

    public void PlayLightningSound() {
        soundSource.PlayOneShot(lightningSound);
    }

    public void PlayExplosionSound() {
        soundSource.PlayOneShot(explosionSound);
    }

    public void PlayBallSound() {
        soundSource.PlayOneShot(ballSound);
    }
}
