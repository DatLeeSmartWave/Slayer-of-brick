using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems; // Thêm namespace này

public class BallSpawner : MonoBehaviour {
    private RaycastHit2D ray;
    [SerializeField] private LayerMask layerMask;
    private float angle;
    [SerializeField] private Vector2 minMaxAngle;
    [SerializeField] bool useRay;
    [SerializeField] bool useLine;
    [SerializeField] bool useDots;
    [SerializeField] LineRenderer line;
    [SerializeField] GameObject ballPrefabs;
    [SerializeField] float force;
    [SerializeField] int ballCount;
    int fistBallCount;
    [SerializeField] TextMeshPro ballCountText;
    [SerializeField] GameObject bricksZone;
    public float distanceToMoveDown;

    private bool isPointerOverUI; // Biến cờ để kiểm tra nếu chuột đang trên UI

    private void Awake() {
        Application.targetFrameRate = 60;
        ballCountText.text = "x" + ballCount.ToString();
        fistBallCount = ballCount;
    }

    private void Update() {
        // Cập nhật trạng thái chuột có nằm trên UI không
        isPointerOverUI = IsPointerOverUIElement();

        // Kiểm tra nếu chuột không nằm trên UI và nút chuột trái được nhả
        if (Input.GetMouseButtonUp(0) && ballCount > 0 && !isPointerOverUI) {
            StartCoroutine(ShootBalls());
        }
    }

    private void FixedUpdate() {
        if (Input.GetMouseButton(0) && !isPointerOverUI) {
            ray = Physics2D.Raycast(transform.position, transform.up, 100f, layerMask);
            Vector2 reflactPos = Vector2.Reflect(new Vector3(ray.point.x, ray.point.y) - transform.position, ray.normal);
            Vector3 pos = Camera.main.WorldToScreenPoint(transform.position);
            Vector3 dir = Input.mousePosition - pos;
            angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg - 90f;

            if (angle >= minMaxAngle.x && angle <= minMaxAngle.y) {
                if (useRay) {
                    Debug.DrawRay(transform.position, transform.up * ray.distance, Color.red);
                    Debug.DrawRay(ray.point, reflactPos.normalized * 2f, Color.green);
                }
                if (useLine) {
                    line.SetPosition(0, transform.position);
                    line.SetPosition(1, ray.point);
                    line.SetPosition(2, ray.point + reflactPos * 2f);
                }
                if (useDots) {
                    Dots.instance.DrawDottedLine(transform.position, ray.point);
                    Dots.instance.DrawDottedLine(ray.point, ray.point + reflactPos * 2f);
                }
            }
            transform.rotation = Quaternion.AngleAxis(angle, transform.forward);
        }
    }

    // Hàm kiểm tra nếu chuột đang nằm trên UI
    private bool IsPointerOverUIElement() {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Count > 0;
    }

    public IEnumerator ShootBalls() {
        for (int i = 0; i < ballCount; i++) {
            yield return new WaitForSeconds(0.1f);
            GameObject ball = Instantiate(ballPrefabs, transform.position, Quaternion.identity);
            ball.GetComponent<Rigidbody2D>().AddForce(transform.up * force);
            ballCount--;
            ballCountText.text = "x" + ballCount.ToString();
        }
    }

    public IEnumerator PlusBall() {
        ballCount++;
        ballCountText.text = "x" + ballCount.ToString();
        if (ballCount == fistBallCount) {
            yield return new WaitForSeconds(1f);
            bricksZone.transform.position = new Vector2(bricksZone.transform.position.x, bricksZone.transform.position.y - distanceToMoveDown);
            PlayerPrefs.SetInt(StringManager.forbidToShootBalls, 0);
        }
    }

    public void AddMoreBalls() {
        ballCount += 5;
        ballCountText.text = "x" + ballCount.ToString();
        PlayerPrefs.SetInt(StringManager.forbidToShootBalls, 0);
    }
}
