using UnityEngine;

public class BottomEdge : MonoBehaviour
{
    [SerializeField] UiPanelDotween lostPanel;

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.CompareTag("Brick")) {
            FindObjectOfType<SoundManager>().PlayLoseSound();
            lostPanel.PanelFadeIn();
            Debug.Log("lost");
        }
    }
}
