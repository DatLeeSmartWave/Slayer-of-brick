using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HomeSceneUi : MonoBehaviour {
    [SerializeField] private Image fadeImage;

    private void Awake() {
        Application.targetFrameRate = 60;
    }

    /// Button

    

    public void LoadLevel(int levelId) {
        PlayerPrefs.SetInt(StringManager.levelId, levelId);
        LoadScene("PlayScene");
    }

    /// Function
   
    
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
