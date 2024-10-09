using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlaySceneUi : MonoBehaviour {

    [SerializeField] private GameObject[] levelObjects;
    [SerializeField] private Image fadeImage;


    private void Awake() {
        Application.targetFrameRate = 60;
    }

    private void Start() {
        ShowLevelObject();
    }

    /// Button 

    public void MinusBrickHpButton() {
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
        if (bricks.Length > 0) {
            Brick[] allBricks = FindObjectsOfType<Brick>();
            foreach (Brick brick in allBricks) {
                brick.MinusBrickHp();
                brick.SpawnLightning();
            }
            Debug.Log("Minus brick hp");
        }
    }

    public void AddMoreBallsButton() {
        FindObjectOfType<BallSpawner>().AddMoreBalls();
    }

    /// Function

    void ShowLevelObject() {
        int levelId = PlayerPrefs.GetInt(StringManager.levelId);
        if (levelId >= 0 && levelId < levelObjects.Length) {
            levelObjects[levelId].SetActive(true);
        }
    }

    // Chuyển cảnh với hiệu ứng fade
    public void LoadScene(string sceneName) {
        //FindObjectOfType<SoundManager>().PlayClickSound();
        StartCoroutine(FadeAndLoadScene(sceneName));
    }

    private IEnumerator FadeAndLoadScene(string sceneName) {
        fadeImage.gameObject.SetActive(true);
        float currentTime = 0f;

        while (currentTime < .5f) {
            currentTime += Time.deltaTime;
            float alpha = Mathf.Lerp(0, 1, currentTime / .5f);
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        SceneManager.LoadScene(sceneName);
    }
}

