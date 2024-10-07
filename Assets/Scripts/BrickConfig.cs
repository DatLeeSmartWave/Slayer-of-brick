using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "BrickConfig", menuName = "ScriptableObjects/BrickConfig", order = 1)]
public class BrickConfig : ScriptableObject {
    [System.Serializable]
    public class BrickData {
        public Vector2Int position; // Vị trí của gạch trong grid (dựa trên hàng và cột)
        public BrickType brickType; // Loại gạch tại vị trí đó
    }

    // Danh sách các gạch (vị trí và loại gạch)
    public List<BrickData> bricks;

    // Trả về loại gạch tại vị trí cụ thể trong grid
    public BrickType GetBrickAtPosition(int row, int column) {
        foreach (var brick in bricks) {
            if (brick.position.x == row && brick.position.y == column) {
                return brick.brickType; // Nếu tìm thấy, trả về loại gạch
            }
        }
        return null; // Nếu không tìm thấy gạch nào tại vị trí, trả về null
    }
}
