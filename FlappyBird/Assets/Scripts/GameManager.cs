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
    public int bonusScore = 0;
    public int level = 1;
    public bool[] isBonus = new bool[5];

    public void SetLevel()
    {
        level = (score / 100 + 1) <= 4 ? (score / 100 + 1) : 4;
    }

    public void SetBonus(int curAlpha)
    {
        isBonus[curAlpha] = true;
    }
    public void ResetBonus()
    {
        for(int idx=  0; idx<isBonus.Length; idx++)
            isBonus[idx] = false;
    }
    public bool CheckBonus()
    {
        for (int idx = 0; idx < isBonus.Length; idx++)
            if (!isBonus[idx]) return false;
        return true;
    }

    public int GetTotalScore()
    {
        return score + bonusScore;
    }
    public void ResetGame()
    {
        score = 0;
        bonusScore = 0;
        isGameOver = false;
        level = 1;
        for (int idx = 0; idx < isBonus.Length; idx++)
            isBonus[idx] = false;
    }
}
