using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeSceneUi : MonoBehaviour {
    [SerializeField] private Image fadeImage;
    [SerializeField] private Image avatar;
    [SerializeField] private Image avatar2;
    [SerializeField] private Sprite[] icons;
    [SerializeField] GameObject changeProfilePanel;
    [SerializeField] TextMeshProUGUI rubyNumberText;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] GameObject profilePanel;
    
    public int rubyNumber;
    [SerializeField] private TextMeshProUGUI starNumberText;
    [SerializeField] private TextMeshProUGUI starNumberText2;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI nameText;
    [SerializeField] private TextMeshProUGUI nameText2;
    [SerializeField] private UiPanelDotween noticePanelObject;
    [SerializeField] private UiPanelDotween leaderBoardPanelObject;
    public TextMeshProUGUI noticeText; 

    private void Awake() {
        Application.targetFrameRate = 60;
        ShowIcon();
        rubyNumber = PlayerPrefs.GetInt(StringManager.rubyNumber,100);
        PlayerPrefs.SetInt(StringManager.rubyNumber, rubyNumber);
        if (rubyNumberText != null)
            rubyNumberText.text = rubyNumber.ToString();
        if (starNumberText != null)
            starNumberText.text = PlayerPrefs.GetInt(StringManager.starNumber).ToString();
        if (profilePanel.activeSelf) {
            levelText.text = (PlayerPrefs.GetInt(StringManager.currentLevelId) + 1).ToString();
            starNumberText2.text = PlayerPrefs.GetInt(StringManager.starNumber).ToString();
            scoreText.text = PlayerPrefs.GetInt(StringManager.score).ToString();
            nameText2.text = PlayerPrefs.GetString(StringManager.playerName);
            Debug.Log(PlayerPrefs.GetString(StringManager.playerName));
        }
        StartCoroutine(ShowPanel());
        //if (changeProfilePanel.activeSelf) {
        //    nameText.text = PlayerPrefs.GetString(StringManager.playerName);
        //    nameText2.text = PlayerPrefs.GetString(StringManager.playerName);
        //}
        
    }

    /// Button

    public void ChooseIcon(int iconId) {
        PlayerPrefs.SetInt(StringManager.iconId, iconId);
        ShowIcon();
    }

    public void LoadLevel(int levelId) {
        PlayerPrefs.SetInt(StringManager.levelId, levelId);
        LoadScene("PlayScene");
    }

    public void BuyRubyButton(int number) {
        rubyNumber += number;
        PlayerPrefs.SetInt(StringManager.rubyNumber, rubyNumber);
        rubyNumberText.text = rubyNumber.ToString();
    }

    public void SaveNameButton() {
        PlayerPrefs.SetString(StringManager.playerName, nameText.text);
        nameText.text = PlayerPrefs.GetString(StringManager.playerName);
        nameText2.text = PlayerPrefs.GetString(StringManager.playerName);
        Debug.Log(PlayerPrefs.GetString(StringManager.playerName));
        //PlayerPrefs.SetString(StringManager.playerName, nameText2.text);
    }

    /// Function

    IEnumerator ShowPanel() {
        yield return new WaitForSeconds(.1f);
        leaderBoardPanelObject.PanelFadeIn();
    }

    public void MinusRubyNumber(int number) {
        rubyNumber -= number;
        PlayerPrefs.SetInt(StringManager.rubyNumber, rubyNumber);
        rubyNumberText.text = rubyNumber.ToString();
    }

    void ShowIcon() {
        int iconId = PlayerPrefs.GetInt(StringManager.iconId);
        if (iconId >= 0 && iconId < icons.Length) {
            avatar.sprite = icons[iconId];
            if (changeProfilePanel.activeSelf)
                avatar2.sprite = icons[iconId];
        }
    }

    // Chuyển cảnh với hiệu ứng fade
    public void LoadScene(string sceneName) {
        //FindObjectOfType<SoundManager>().PlayClickSound();
        StartCoroutine(FadeAndLoadScene(sceneName));
    }

    private IEnumerator FadeAndLoadScene(string sceneName) {
        if (fadeImage != null)
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

    public void ShowNoticePanel() {
        noticePanelObject.PanelFadeIn();
    }
}
