using UnityEngine;

public class EdgeScaler : MonoBehaviour {
    [SerializeField] GameObject playerPos; // Gán đối tượng người chơi
    [SerializeField] GameObject bottomEdge; // Gán cạnh dưới
    public float distanceToMove; // Khoảng cách để di chuyển người chơi

    private void Awake() {
        // Lấy kích thước màn hình
        float screenHeight = Camera.main.orthographicSize * 2;
        float screenWidth = screenHeight * Camera.main.aspect;

        // Đặt vị trí cho người chơi cách cạnh dưới một khoảng distanceToMove
        if (gameObject.CompareTag("BottomEdge")) {
            playerPos.transform.position = new Vector2(playerPos.transform.position.x, bottomEdge.transform.position.y + distanceToMove);
        }

        // Điều chỉnh kích thước cho các cạnh
        if (gameObject.CompareTag("TopEdge") || gameObject.CompareTag("BottomEdge")) {
            // Kéo dài cạnh theo chiều ngang
            transform.localScale = new Vector3(screenWidth, transform.localScale.y, transform.localScale.z);
            // Đặt vị trí cạnh trên hoặc dưới
            transform.position = new Vector3(0, (gameObject.CompareTag("TopEdge") ? screenHeight / 2 : -screenHeight / 2 + 1f), 0);
        } else if (gameObject.CompareTag("LeftEdge") || gameObject.CompareTag("RightEdge")) {
            // Kéo dài cạnh theo chiều cao
            transform.localScale = new Vector3(transform.localScale.x, screenHeight, transform.localScale.z);
            // Đặt vị trí cạnh trái hoặc phải
            transform.position = new Vector3((gameObject.CompareTag("LeftEdge") ? -screenWidth / 2 : screenWidth / 2), 0, 0);
        }
    }

    void Start() {
        
    }
}
