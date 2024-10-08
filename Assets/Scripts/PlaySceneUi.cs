using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneUi : MonoBehaviour {
    /// Button 

    public void MinusBrickHpButton() {
        GameObject[] bricks = GameObject.FindGameObjectsWithTag("Brick");
        if (bricks.Length > 0) {
            Brick[] allBricks = FindObjectsOfType<Brick>();
            foreach (Brick brick in allBricks) {
                brick.MinusBrickHp();
                brick.SpawnLightning();
            }
            Debug.Log("Minus brick hp");
        }
    }

    public void AddMoreBallsButton() {
        FindObjectOfType<BallSpawner>().AddMoreBalls();
    }
}
