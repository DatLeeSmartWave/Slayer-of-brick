using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Brick : MonoBehaviour {
    [SerializeField] TextMeshPro brickHPText;
    [SerializeField] GameObject explosionPrefab;
    [SerializeField] GameObject lightningPrefab;
    public int brickHP;
    // Start is called before the first frame update
    void Start() {
        brickHPText.text = brickHP.ToString();
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Ball") {
            brickHP--;
            brickHPText.text = brickHP.ToString();
            if (brickHP <= 0) {
                int score;
                score = PlayerPrefs.GetInt(StringManager.score);
                score += 10;
                PlayerPrefs.SetInt(StringManager.score, score);
                FindObjectOfType<PlaySceneUi>().PlusProgressSlidervalue(.2f);
                gameObject.SetActive(false);
            }
        } 
        else if (collision.gameObject.tag == "Barrier") {
            int score;
            score = PlayerPrefs.GetInt(StringManager.score);
            score += 10;
            PlayerPrefs.SetInt(StringManager.score, score);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            FindObjectOfType<SoundManager>().PlayExplosionSound();
            FindObjectOfType<PlaySceneUi>().PlusProgressSlidervalue(.3f);
            gameObject.SetActive(false);
            Destroy(collision.gameObject);
        }
    }

    public void MinusBrickHp() {
        brickHP -= 10;
        FindObjectOfType<SoundManager>().PlayLightningSound();
        brickHPText.text = brickHP.ToString();
        if (brickHP <= 0) {
            int score;
            score = PlayerPrefs.GetInt(StringManager.score);
            score += 10;
            PlayerPrefs.SetInt(StringManager.score, score);
            gameObject.SetActive(false);
        }
    }

    public void SpawnLightning() {
        Instantiate(lightningPrefab, transform.position, Quaternion.identity);
    }
}
