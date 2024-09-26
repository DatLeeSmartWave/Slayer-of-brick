using UnityEngine;

public class EdgeScaler : MonoBehaviour {
    void Start() {
        // Lấy kích thước của màn hình trong world units
        float screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = screenHeight * Camera.main.aspect;

        // Điều chỉnh kích thước cho các cạnh
        if (gameObject.CompareTag("TopEdge") || gameObject.CompareTag("BottomEdge")) {
            // Đối với cạnh trên và dưới, kéo dài theo chiều ngang
            transform.localScale = new Vector3(screenWidth, transform.localScale.y, transform.localScale.z);
            // Di chuyển vị trí tới cạnh trên hoặc dưới
            transform.position = new Vector3(0, (gameObject.CompareTag("TopEdge") ? screenHeight / 2 : -screenHeight / 2), 0);
        } else if (gameObject.CompareTag("LeftEdge") || gameObject.CompareTag("RightEdge")) {
            // Đối với cạnh trái và phải, kéo dài theo chiều cao
            transform.localScale = new Vector3(transform.localScale.x, screenHeight, transform.localScale.z);
            // Di chuyển vị trí tới cạnh trái hoặc phải
            transform.position = new Vector3((gameObject.CompareTag("LeftEdge") ? -screenWidth / 2 : screenWidth / 2), 0, 0);
        }
    }
}
