using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour {
    [SerializeField] Sprite lockedIcon;
    [SerializeField] Sprite ongoingIcon;
    [SerializeField] GameObject yellowStars;
    [SerializeField] GameObject whiteStars;
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] int levelId;
    [SerializeField] Image button;
    [SerializeField] private Image fadeImage;
    // Start is called before the first frame update
    void Start() {
        //if(levelId == 0)
        //    button.sprite = ongoingIcon;
        if (levelText != null)
            levelText.text = (levelId + 1).ToString();
        SetUpButtonStatus();
    }

    /// Function
    void SetUpButtonStatus() {
        if (levelId <= PlayerPrefs.GetInt(StringManager.currentLevelId))
            button.sprite = ongoingIcon;
        else {
            button.sprite = lockedIcon;
            levelText.gameObject.SetActive(false);
            whiteStars.SetActive(false);
        }
        if (levelId < PlayerPrefs.GetInt(StringManager.currentLevelId)) {
            yellowStars.SetActive(true);
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

    /// Button

    public void LoadLevel() {
        if (button.sprite == ongoingIcon) {
            PlayerPrefs.SetInt(StringManager.levelId, levelId);
            LoadScene("PlayScene");
        }
    }
}
