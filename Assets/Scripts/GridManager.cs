using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {
    [SerializeField] private Camera mainCamera;
    [SerializeField] private int rows;
    [SerializeField] private int columns;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private BrickConfig brickConfig; // Tham chiếu đến ScriptableObject quản lý các viên gạch
    [SerializeField] private GameObject pointPrefabs; // Prefab điểm
    [SerializeField] private GameObject brickGrid;
    [SerializeField] private GameObject playerPos;
    [SerializeField] private GameObject barrierPrefab; // Prefab của Barrier

    [SerializeField] private float barrierSpeed = 5f; // Tốc độ bay của Barrier

    private void Awake() {
        Application.targetFrameRate = 60;
        //GenerateGrid();
    }

    private void Start() {
        brickGrid.transform.localPosition = new Vector3(playerPos.transform.localPosition.x - 2, 3.14f, -0.011f);
    }

    private void Update() {
    }

    // Hàm tạo grid với các viên gạch và điểm dựa trên cấu hình từ ScriptableObject
    private void GenerateGrid() {
        for (int row = 0; row < rows; row++) {
            for (int column = 0; column < columns; column++) {
                // Tính toán vị trí cho ô trong grid
                Vector3 position = new Vector3(column * cellSize, row * cellSize, 0);

                // Instantiate brick nếu có gạch tại vị trí này
                BrickType brickType = brickConfig.GetBrickAtPosition(row, column);
                if (brickType != null) {
                    Instantiate(brickType.brickPrefab, position, Quaternion.identity, transform);
                }

                // Instantiate point prefab tại tất cả các vị trí trong grid
                Instantiate(pointPrefabs, position, Quaternion.identity, transform);
            }
        }
    }

    // Hàm cho phép cập nhật số hàng và cột từ bên ngoài
    public void UpdateGridSize(int newRows, int newColumns) {
        rows = newRows;
        columns = newColumns;
        GenerateGrid();
    }

    // Hàm bắn ra một Barrier bay lên cao
    public void ShootBarrier() {
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
        if (bricks.Length > 0) {
            // Tạo một Barrier tại vị trí của playerPos
            GameObject newBarrier = Instantiate(barrierPrefab, playerPos.transform.position, Quaternion.identity);

            // Kiểm tra nếu barrier có Rigidbody2D, nếu có thì đẩy nó bay lên
            Rigidbody2D rb = newBarrier.GetComponent<Rigidbody2D>();
            if (rb != null) {
                rb.velocity = new Vector2(0, barrierSpeed); // Đẩy barrier bay lên theo trục Y
            }
            Destroy(newBarrier, 2f);
        }
    }
}
