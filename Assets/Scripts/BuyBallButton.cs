using TMPro;
using UnityEngine;

public class BuyBallButton : MonoBehaviour {
    [SerializeField] private int ballId;
    [SerializeField] private int ballPrice;
    [SerializeField] private GameObject priceObject;
    [SerializeField] private GameObject useTextIcon;
    [SerializeField] private TextMeshProUGUI ballPriceText;
    [SerializeField] private TextMeshProUGUI rubyNumberText;
    [SerializeField] private GameObject[] redHearts;
    int rubyNumber;

    // Start is called before the first frame update
    void Start() {
        ballPriceText.text = ballPrice.ToString();
        rubyNumber = PlayerPrefs.GetInt(StringManager.rubyNumber);

        // Kiểm tra trạng thái đã lưu của ballId
        if (PlayerPrefs.GetInt("BallPurchased_" + ballId, 0) == 1) {
            // Nếu ball đã được mua trước đó, ẩn giá và hiển thị icon "Use"
            priceObject.SetActive(false);
            useTextIcon.SetActive(true);
        } else {
            // Nếu chưa mua, hiển thị giá
            priceObject.SetActive(true);
            useTextIcon.SetActive(false);
        }
        if (ballId == 0) {
            priceObject.SetActive(false);
            useTextIcon.SetActive(true);
        }
        ChooseBall();
    }

    ///Button
    public void UseBalButton() {
        if (PlayerPrefs.GetInt(StringManager.rubyNumber) >= ballPrice &&
            priceObject.activeSelf) {
            priceObject.SetActive(false);
            useTextIcon.SetActive(true);
            MinusRubyNumber(ballPrice);

            // Lưu trạng thái ball đã được mua
            PlayerPrefs.SetInt("BallPurchased_" + ballId, 1);
            PlayerPrefs.SetInt(StringManager.ballId, ballId);
            ChooseBall();
        } else {
            FindObjectOfType<HomeSceneUi>().ShowNoticePanel();
            FindObjectOfType<HomeSceneUi>().noticeText.text = "You don't have enough Ruby to buy !"; 
        }
        if (useTextIcon.activeSelf) {
            PlayerPrefs.SetInt(StringManager.ballId, ballId);
            ChooseBall();
        }
    }


    /// Function
    void MinusRubyNumber(int number) {
        rubyNumber = PlayerPrefs.GetInt(StringManager.rubyNumber);
        rubyNumber -= number;
        PlayerPrefs.SetInt(StringManager.rubyNumber, rubyNumber);
        rubyNumberText.text = rubyNumber.ToString();
    }

    void ChooseBall() {
        int heartId = PlayerPrefs.GetInt(StringManager.ballId);
        if (heartId >= 0 && heartId < redHearts.Length) {
            foreach (GameObject heart in redHearts) {
                heart.SetActive(false);
            }
            redHearts[heartId].SetActive(true);
        }
    }
}
