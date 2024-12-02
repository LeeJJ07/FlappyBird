using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager sInstance;
    public static GameManager Instance
    {
        get
        {
            if(sInstance == null)
            {
                GameObject newGameObject = new GameObject("@GameManager");
                sInstance = newGameObject.AddComponent<GameManager>();
            }
            return sInstance;
        }
    }

    private void Awake()
    {
        if(sInstance != null && sInstance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        sInstance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    public bool isGameOver = false;
    public int score = 0;
    public int level = 1;

    public void SetLevel()
    {
        level = (score / 100 + 1) <= 4 ? (score / 100 + 1) : 4;
    }
}
