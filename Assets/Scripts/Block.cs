using System.Collections;
using System.Collections.Generic;
using UnityEngine;

enum TypeOfBuff
{
    HP = 0,
    Size = 1,
    Floor = 2
}
public class Block : MonoBehaviour {
    public int index;
    [SerializeField]
    private int hp;
    private SpriteRenderer sprite;
    [SerializeField]
    void Awake() {
        hp = Random.Range(1, 4);
        sprite = this.GetComponent<SpriteRenderer>();
        ChangeColor(hp);
    }

    private void OnCollisionEnter2D(Collision2D collision) {
        hp--;
        ChangeColor(hp);
        Messenger<int>.Broadcast(GameEvent.Score, 4);
        if (hp == 0) {
            Destroy(this.gameObject);
        }
    }

    private void ChangeColor(int x) {
        switch (x) {
            case 1:
                sprite.color = new Color(255, 0, 0, 1);
                break;
            case 2:
                sprite.color = new Color(0, 255, 0, 1);
                break;
            case 3:
                sprite.color = new Color(0, 0, 255, 1);
                break;
        }
    }

    private void OnDestroy() {
        if (UIController.IsActive) {
            int x = Random.Range(0, 15);
            switch (x) {
                case 0:
                case 1:
                case 2:
                    Messenger<TypeOfBuff, Vector3>.Broadcast(GameEvent.Buff, GetRandomBuff(), this.transform.position);
                    break;
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                case 10:
                case 11:
                case 12:
                case 13:
                case 14:
                    break;

            }
        }
    }

    private TypeOfBuff GetRandomBuff() {
        int x = Random.Range(0, 3);
        switch (x) {
            case 0:
                return TypeOfBuff.HP;
            case 1:
                return TypeOfBuff.Floor;

            case 2:
                return TypeOfBuff.Size;
        }
        return TypeOfBuff.Size;
    }
}
