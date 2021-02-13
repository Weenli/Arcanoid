using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {
    public int lifeCount;
    [SerializeField]
    private GameController _gameController;
    private const float speed = 30.0f;
    [SerializeField]
    private GameObject[] UIElem;
    [SerializeField]
    private RectTransform TargetPlatform;
    private Vector3 PlatformStartPos;
    private Rigidbody2D Body;
    private Vector3 StartPos;
    private int CountToNewRaw;
    private void Start() {
        Time.timeScale = 0;
        Body = this.GetComponent<Rigidbody2D>();
        lifeCount = 3;
        CountToNewRaw = -1;
        PlatformStartPos = TargetPlatform.transform.position;
        StartPos = this.transform.position;
    }

    private void LateUpdate() {
        if (Mathf.Abs(Body.velocity.y) < 0.8) {
            Body.velocity = new Vector3(Body.velocity.x, 1.0f / 1);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        if (collision.gameObject.tag == "Platform") {
            float Direction = (this.transform.position.x - collision.transform.position.x) / (2 * TargetPlatform.rect.width);
            Vector3 dir = new Vector3(Direction, 1).normalized;
            Body.velocity = dir * speed;
            CountToNewRaw++;
            if (CountToNewRaw == 3) {
                _gameController.InsertLine();
                CountToNewRaw = 0;
            }
        }
        else if (collision.gameObject.tag == "DeathZone") {
            Time.timeScale = 0;
            foreach (GameObject s in UIElem) {
                s.SetActive(true);
            }
            lifeCount--;
            Messenger<int>.Broadcast(GameEvent.Hp, lifeCount);
            this.transform.position = StartPos;
            TargetPlatform.position = PlatformStartPos;
            if (lifeCount == 0) {
                Messenger.Broadcast(GameEvent.GameOver);
            }
        }
    }

    private void StartGame() {
        Body.velocity = Vector3.up * speed;
        Time.timeScale = 1;
    }
}
