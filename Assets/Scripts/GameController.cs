using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {
    [Header("GridComponent")]
    private const int Row = 4;
    private const int Column = 6;
    [SerializeField]
    private GameObject BlockPrefab;
    private List<GameObject> Grid = new List<GameObject>();
    [Header("Buffs")]
    [SerializeField]
    private GameObject[] Buffs;
    [SerializeField]
    private float Height;
    private float Width;
    void Start() {
        Messenger<TypeOfBuff, Vector3>.AddListener(GameEvent.Buff, SpawnBuff);
        Width = BlockPrefab.transform.localScale.x * 8.0f;
        Height = Width * Screen.height / Screen.width;
        GenerateBoard();

    }

    void LateUpdate() {
        if (Grid.Count == 0) {
            InsertOneLine(0);
        }
        for (int i = 0; i < Grid.Count; i++) {
            if (Grid[i] == null) {
                Grid.RemoveAt(i);
            }
        }
    }

    private void GenerateBoard() {
        for (int i = 0; i < 4; i++) {
            InsertOneLine(i);
        }
    }

    public void InsertLine() {
        AllLinesDown();
        InsertOneLine(0);
    }

    private void AllLinesDown() {
        for (int i = 0; i < Grid.Count; i++) {

            Grid[i].transform.position = new Vector3(Grid[i].transform.position.x, Grid[i].transform.position.y - Width / 8);
            if (Grid[i].transform.position.y <= -1.0f * Height / 8.0f) {
                Messenger.Broadcast(GameEvent.GameOver);
            }
        }
    }

    private void InsertOneLine(int row) {

        int col = 0;
        int index = 0;

        for (int i = 0; i < Column; i++) {
            int rand = Random.Range(0, 8);
            switch (rand) {
                case 0:
                case 1:
                case 2:
                    break;
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:

                    GameObject clone;
                    clone = Instantiate(BlockPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
                    clone.transform.SetParent(this.transform);
                    clone.transform.position = new Vector3(-5.0f * Width / 16.0f + col * clone.transform.localScale.x + 0.25f, 6 * Height / 16 - row * clone.transform.localScale.y, 0);
                    clone.name = "Block" + Grid.Count.ToString();
                    Grid.Insert(Grid.Count, clone);
                    index++;
                    break;
            }
            col++;
        }
    }

    private void Restart() {
        SceneManager.LoadScene(0);
    }

    private void Exit() {
        Application.Quit();
    }

    private void SpawnBuff(TypeOfBuff obj, Vector3 pos) {
        GameObject Buff;
        switch (obj) {
            case TypeOfBuff.HP:
                Buff = Instantiate(Buffs[0], pos, Quaternion.identity);
                break;
            case TypeOfBuff.Floor:
                Buff = Instantiate(Buffs[1], pos, Quaternion.identity);
                break;
            case TypeOfBuff.Size:
                Buff = Instantiate(Buffs[2], pos, Quaternion.identity);
                break;

        }
    }

    private void OnDestroy() {
        Messenger<TypeOfBuff, Vector3>.RemoveListener(GameEvent.Buff, SpawnBuff);
    }
}
