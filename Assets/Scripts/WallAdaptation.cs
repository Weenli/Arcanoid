using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallAdaptation : MonoBehaviour {
    private Camera _cam;
    [SerializeField]
    private RectTransform[] Walls = new RectTransform[2];
    [SerializeField]
    private RectTransform Roof;
    [SerializeField]
    private RectTransform Platform;
    [SerializeField]
    private RectTransform[] Floor;
    [SerializeField]
    private Transform Ball;
    [SerializeField]
    private Transform BlockPrefab;
    void Awake() {
        _cam = this.transform.GetComponent<Camera>();
        float height = _cam.orthographicSize * 2.0f;
        float width = height * Screen.width / Screen.height;
        Vector3 scale = new Vector3(1, height / Walls[0].rect.height, 1);
        Walls[0].position = new Vector3(-width / 2, 0, Walls[0].position.z);
        Walls[1].position = new Vector3(width / 2, 0, Walls[1].position.z);
        Roof.localScale = new Vector3(width / Roof.rect.width, 1, 1);
        Roof.position = new Vector3(0, height / 2, Roof.position.z);
        Floor[0].localScale = Roof.localScale;
        Floor[0].position = new Vector3(0, -height / 2 - 1.9f, Roof.position.z);
        Floor[1].localScale = Roof.localScale;
        Floor[1].position = new Vector3(0, -height / 2, Roof.position.z);
        Ball.transform.localScale = new Vector3(0.1f * width / 2, 0.1f * width / 2);
        BlockPrefab.localScale = new Vector3(width / 8, width / 8);
        foreach (Transform s in Walls) {
            s.localScale = scale;
        }
        Platform.position = new Vector3(-1, -7.0f * height / 16.0f, 1);
        Ball.position = new Vector3(0, Platform.position.y + Ball.transform.localScale.x + 0.25f, 1);
        Platform.localScale = new Vector3(width / 5, height / 30, 1);
    }
}
