using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Brick : MonoBehaviour {
    [SerializeField] TextMeshPro brickHPText;
    public int brickHP;
    // Start is called before the first frame update
    void Start() {
        brickHPText.text = brickHP.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ball") {
            brickHP--;
            brickHPText.text = brickHP.ToString();
            if (brickHP <= 0)
                gameObject.SetActive(false);
        } 
        else if (collision.gameObject.tag == "Barrier") {
            gameObject.SetActive(false);
            Destroy(collision.gameObject);
        }
    }

    public void MinusBrickHp() {
        brickHP-=10;
        brickHPText.text = brickHP.ToString();
        if (brickHP <= 0)
            gameObject.SetActive(false);
        PlayerPrefs.SetInt(StringManager.forbidToShootBalls, 0);
    }
}
