using UnityEngine;
using System.Collections;

public class GameOverBehaviour : MonoBehaviour
{
    public GameObject game;
    void Start()
    {
        Game_Actions.Instance.over += over;
    }

    void OnDisable()
    {
        Game_Actions.Instance.over -= over;
    }

    void over()
    {
        Debug.Log("GameOver");
        this.game.SetActive(true);
    }
}
