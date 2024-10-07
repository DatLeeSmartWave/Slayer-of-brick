using System.Collections.Generic;
using UnityEngine;

public class GridManager : MonoBehaviour {
    [SerializeField] private Camera mainCamera;
    [SerializeField] private int rows;
    [SerializeField] private int columns;
    [SerializeField] private float cellSize = 1f;
    [SerializeField] private BrickConfig brickConfig; // Tham chiếu đến ScriptableObject quản lý các viên gạch

    private void Awake() {
        Application.targetFrameRate = 60;
        //mainCamera.transform.position = new Vector3(1.41f,mainCamera.transform.position.y,mainCamera.transform.position.z);
        GenerateGrid();
    }

    private void Start() {
    }

    // Hàm tạo grid với các viên gạch dựa trên cấu hình từ ScriptableObject
    private void GenerateGrid() {
        for (int row = 0; row < rows; row++) {
            for (int column = 0; column < columns; column++) {
                // Kiểm tra xem có gạch tại vị trí này không
                BrickType brickType = brickConfig.GetBrickAtPosition(row, column);
                if (brickType != null) {
                    Vector3 position = new Vector3(column * cellSize, row * cellSize, 0);
                    Instantiate(brickType.brickPrefab, position, Quaternion.identity, transform);
                }
            }
        }
    }

    // Hàm cho phép cập nhật số hàng và cột từ bên ngoài
    public void UpdateGridSize(int newRows, int newColumns) {
        rows = newRows;
        columns = newColumns;
        GenerateGrid();
    }
}