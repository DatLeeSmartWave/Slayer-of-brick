using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySceneUi : MonoBehaviour {
    /// Button 

    public void MinusBrickHpButton() {
        Brick[] allBricks = FindObjectsOfType<Brick>();
        foreach (Brick brick in allBricks) {
            brick.MinusBrickHp();
        }
    }

    public void AddMoreBallsButton() {
        FindObjectOfType<BallSpawner>().AddMoreBalls();
    }
}
