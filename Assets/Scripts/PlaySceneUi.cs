using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlaySceneUi : MonoBehaviour {

    [SerializeField] private GameObject[] levelObjects;
    [SerializeField] private Image fadeImage;
    [SerializeField] private UiPanelDotween winpanel;
    [SerializeField] private TextMeshProUGUI levelText;
    bool hasFadeIn =false ;

    private void Awake() {
        Application.targetFrameRate = 60;
    }

    private void Start() {
        ShowLevelObject();
    }

    private void Update() {
        //ShowWinPanel();
        CountBrickNumber();
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

    public void NextLevelButton() {
        LoadScene("PlayScene");
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
        if (levelText != null)
            levelText.text = "Level " + (levelId+1).ToString();
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

    void CountBrickNumber() {
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
        if (bricks.Length == 0) {
            if(winpanel!=null)
            ShowWinPanel();
        }
    }

    void ShowWinPanel() {
        if (!hasFadeIn) {
            winpanel.PanelFadeIn();
            PlayerPrefs.SetInt(StringManager.levelId, PlayerPrefs.GetInt(StringManager.levelId) + 1);
            PlayerPrefs.SetInt(StringManager.currentLevelId, PlayerPrefs.GetInt(StringManager.levelId));
            Debug.Log("Next level: " + (PlayerPrefs.GetInt(StringManager.currentLevelId) ));
            hasFadeIn = true;
        }
    }
}

