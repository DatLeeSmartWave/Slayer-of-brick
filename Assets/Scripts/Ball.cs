using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    private Rigidbody2D rb;
    private GameObject player;

    private void Start() {
        // Lấy thành phần Rigidbody2D và vật thể có tag là "Player"
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");

        if (player == null) {
            Debug.LogError("Không tìm thấy vật thể với tag 'Player'");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "BottomEdge") {
            Vector2 directionToPlayer = (player.transform.position - transform.position).normalized;
            rb.velocity = Vector2.zero;
            float forceMagnitude = 10f; 
            rb.AddForce(directionToPlayer * forceMagnitude, ForceMode2D.Impulse);
        } else if (collision.gameObject.tag == "Player") {
            gameObject.SetActive(false);
            FindObjectOfType<BallSpawner>().PlusBall();
            Debug.Log("hide");
        }
    }
}
