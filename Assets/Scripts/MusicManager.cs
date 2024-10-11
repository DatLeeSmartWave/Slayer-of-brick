using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MusicManager : MonoBehaviour {
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private Image musicButton;
    [SerializeField] private Sprite musicOn, musicOff;
    int musicId;

    // Start is called before the first frame update
    void Start() {
        musicId = PlayerPrefs.GetInt(StringManager.musicId, 1);
        PlayerPrefs.SetInt(StringManager.musicId, musicId);
        if (SceneManager.GetActiveScene().name == "HomeScene") {
            
            InitButtonStatus();
        }
        if(PlayerPrefs.GetInt(StringManager.musicId) == 1)
            musicSource.volume = 1;
        else
            musicSource.volume = 0;
    }

    void InitButtonStatus() {
        if (PlayerPrefs.GetInt(StringManager.musicId) == 1) 
            musicButton.sprite = musicOn;
        else
            musicButton.sprite = musicOff;
    }

    public void SwitchButton() {
        if (musicButton.sprite == musicOn) {
            musicButton.sprite = musicOff;
            PlayerPrefs.SetInt(StringManager.musicId, 0);
            musicSource.volume = 0;
        } else {
            musicButton.sprite = musicOn;
            PlayerPrefs.SetInt(StringManager.musicId, 1);
            musicSource.volume = 1;
        }
    }
}
