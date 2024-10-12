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
    [SerializeField] private Image avatar;
    [SerializeField] private Sprite[] icons;
    [SerializeField] private TextMeshProUGUI rubyNumberText;
    int rubyNumber;
    [SerializeField] private TextMeshProUGUI starNumberText;
    int starNumber;
    [SerializeField] private UiPanelDotween noticePanelObject;
    bool hasFadeIn = false;
    [SerializeField] GameObject winPanel;
    [SerializeField] private TextMeshProUGUI scoreText2;
    [SerializeField] private Image progressSlider;
    [SerializeField] private GameObject fireworkEffect;

    private void Awake() {
        Application.targetFrameRate = 60;
        rubyNumber = PlayerPrefs.GetInt(StringManager.rubyNumber);
        if (rubyNumberText != null)
            rubyNumberText.text = rubyNumber.ToString();
        if (starNumberText != null)
            starNumberText.text = PlayerPrefs.GetInt(StringManager.starNumber).ToString();
    }

    private void Start() {
        ShowLevelObject();
        ShowIcon();
    }

    private void Update() {
        //ShowWinPanel();
        CountBrickNumber();
    }

    public void PlusProgressSlidervalue(float value) {
        progressSlider.fillAmount += value;
    }

    /// Button 

    public void MinusBrickHpButton() {
        if (rubyNumber > 0) {
            GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
            if (bricks.Length > 0) {
                Brick[] allBricks = FindObjectsOfType<Brick>();
                foreach (Brick brick in allBricks) {
                    brick.MinusBrickHp();
                    brick.SpawnLightning();
                }
                rubyNumber -= 5;
                PlayerPrefs.SetInt(StringManager.rubyNumber, rubyNumber);
                rubyNumberText.text = rubyNumber.ToString();
            }
        } else
            ShowNoticePanel();
    }

    public void NextLevelButton() {
        PlayerPrefs.SetInt(StringManager.levelId, PlayerPrefs.GetInt(StringManager.levelId) + 1);
        PlayerPrefs.SetInt(StringManager.currentLevelId, PlayerPrefs.GetInt(StringManager.levelId));
        LoadScene("PlayScene");
    }

    public void HomeSceneButton() {
        PlayerPrefs.SetInt(StringManager.levelId, PlayerPrefs.GetInt(StringManager.levelId) + 1);
        PlayerPrefs.SetInt(StringManager.currentLevelId, PlayerPrefs.GetInt(StringManager.levelId));
        LoadScene("HomeScene");
    }

    public void AddMoreBallsButton() {
        if (rubyNumber > 0) {
            FindObjectOfType<BallSpawner>().AddMoreBalls();
            rubyNumber -= 5;
            PlayerPrefs.SetInt(StringManager.rubyNumber, rubyNumber);
            rubyNumberText.text = rubyNumber.ToString();
        } else
            ShowNoticePanel();
    }

    public void DestroyRow() {
        if (rubyNumber > 0) {
            FindObjectOfType<GridManager>().ShootBarrier();
            rubyNumber -= 5;
            PlayerPrefs.SetInt(StringManager.rubyNumber, rubyNumber);
            rubyNumberText.text = rubyNumber.ToString();
        } else
            ShowNoticePanel();
    }

    /// Function

    void ShowNoticePanel() {
        noticePanelObject.PanelFadeIn();
    }

    void ShowLevelObject() {
        int levelId = PlayerPrefs.GetInt(StringManager.levelId);
        if (levelId >= 0 && levelId < levelObjects.Length) {
            levelObjects[levelId].SetActive(true);
        }
        if (levelText != null)
            levelText.text = "Level " + (levelId + 1).ToString();
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
            if (winpanel != null)
                ShowWinPanel();
        }
    }

    void ShowWinPanel() {
        if (!hasFadeIn) {
            winpanel.PanelFadeIn();
            rubyNumber += 20;
            PlayerPrefs.SetInt(StringManager.rubyNumber, rubyNumber);
            rubyNumberText.text = rubyNumber.ToString();
            int newStarNumber = PlayerPrefs.GetInt(StringManager.starNumber) + 3;
            PlayerPrefs.SetInt(StringManager.starNumber, newStarNumber);
            Debug.Log(PlayerPrefs.GetInt(StringManager.starNumber));
            starNumberText.text = newStarNumber.ToString();
            FindObjectOfType<SoundManager>().PlayWinSound();
            if (winPanel.activeSelf)
                scoreText2.text = PlayerPrefs.GetInt(StringManager.score).ToString();
            fireworkEffect.SetActive(true);
            hasFadeIn = true;
        }
    }

    void ShowIcon() {
        int iconId = PlayerPrefs.GetInt(StringManager.iconId);
        if (iconId >= 0 && iconId < icons.Length) {
            avatar.sprite = icons[iconId];
        }
    }
}

