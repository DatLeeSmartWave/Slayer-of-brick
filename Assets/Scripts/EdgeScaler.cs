using UnityEngine;

public class EdgeScaler : MonoBehaviour {
    [SerializeField] GameObject playerPos;
    [SerializeField] GameObject bottomEdge;

    void Start() {
        float screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = screenHeight * Camera.main.aspect;
        if(gameObject.tag == "BottomEdge")
        playerPos.transform.position = new Vector2(playerPos.transform.position.x, bottomEdge.transform.position.y +1.25f);

        // Điều chỉnh kích thước cho các cạnh
        if (gameObject.CompareTag("TopEdge") || gameObject.CompareTag("BottomEdge")|| gameObject.CompareTag("Player")) {
            transform.localScale = new Vector3(screenWidth, transform.localScale.y, transform.localScale.z);
            transform.position = new Vector3(0, (gameObject.CompareTag("TopEdge") ? screenHeight / 2 : -screenHeight / 2), 0);
        } else if (gameObject.CompareTag("LeftEdge") || gameObject.CompareTag("RightEdge")) {
            transform.localScale = new Vector3(transform.localScale.x, screenHeight, transform.localScale.z);
            transform.position = new Vector3((gameObject.CompareTag("LeftEdge") ? -screenWidth / 2 : screenWidth / 2), 0, 0);
        }
    }
}
