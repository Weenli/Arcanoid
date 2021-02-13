using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {
    [SerializeField]
    private Ball Ball;
    private const float speed = 30.0f;
    private Rigidbody2D Body;
    [SerializeField]
    private GameObject Floor;
    private Vector3 StandartScale;
    private bool IsPressedRight = false;
    private bool IsPressedLeft = false;
    void Start() {
        Body = this.GetComponent<Rigidbody2D>();
        StandartScale = this.transform.localScale;
    }

    // Update is called once per frame
    void Update() {
        float Horiz = Input.GetAxis("Horizontal");
        if (Time.timeScale != 0) {
            Body.velocity = Vector3.right * Horiz * speed;
        }
        if (IsPressedLeft) {
            Body.velocity = -Vector3.right * speed;
        }
        if (IsPressedRight) {
            Body.velocity = Vector3.right * speed;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "HpBuff") {
            Ball.lifeCount += 1;
            Messenger<int>.Broadcast(GameEvent.Hp, Ball.lifeCount);
        }
        else if (other.gameObject.tag == "FloorBuff") {
            if (Floor.activeSelf) {
                StopCoroutine("OpenFloor");
                Floor.SetActive(false);
            }
            StartCoroutine("OpenFloor");

        }
        else if (other.gameObject.tag == "SizeBuff") {
            if (this.transform.localScale != StandartScale) {
                StopCoroutine("SetPlatformSize");
                this.transform.localScale = StandartScale;
            }
            StartCoroutine("SetPlatformSize");
        }
        Destroy(other.gameObject);
    }

    private IEnumerator OpenFloor() {
        Floor.SetActive(true);
        yield return new WaitForSeconds(8);
        Floor.SetActive(false);
    }

    private IEnumerator SetPlatformSize() {
        this.transform.localScale = new Vector3(this.transform.localScale.x * 1.5f, this.transform.localScale.y);
        yield return new WaitForSeconds(10);
        this.transform.localScale = StandartScale;
    }

    public void OnMoveRight() {
        if (Time.timeScale != 0) {
            IsPressedRight = true;
        }
    }

    public void OnMoveLeft() {
        if (Time.timeScale != 0) {
            IsPressedLeft = true;
        }
    }

    public void OnTriggerUp() {
        IsPressedLeft = false;
        IsPressedRight = false;
    }

}
