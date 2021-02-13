using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {
    public static bool IsActive { private set; get; }

    [SerializeField]
    private Text Text;
    [SerializeField]
    private Text Score;
    [SerializeField]
    private Text Hp;
    [SerializeField]
    private GameObject[] UIElem;
    [SerializeField]
    private Button StartMask;
    private int _score;

    void Start() {
        Messenger.AddListener(GameEvent.GameOver, GameOver);
        Messenger<int>.AddListener(GameEvent.Score, ScoreUpdate);
        Messenger<int>.AddListener(GameEvent.Hp, UpdateHp);
        _score = 0;
        IsActive = true;
    }

    void Update() {

    }
    private void GameOver() {
        Text.text = "Game Over!";
        Time.timeScale = 0;
        foreach (GameObject s in UIElem) {
            s.SetActive(true);
        }
        StartMask.gameObject.SetActive(true);
        StartMask.interactable = false;
        IsActive = false;
    }
        
    private void ScoreUpdate(int scor) {
        _score += scor;
        Score.text = "Score: " + _score.ToString();
    }

    private void UpdateHp(int hp) {
        Hp.text = "HP: " + hp.ToString();
    }

    private void OnDestroy() {
        Messenger.RemoveListener(GameEvent.GameOver, GameOver);
        Messenger<int>.RemoveListener(GameEvent.Score, ScoreUpdate);
        Messenger<int>.RemoveListener(GameEvent.Hp, UpdateHp);
    }
}