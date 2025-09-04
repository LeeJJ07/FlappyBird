using Unity.VisualScripting;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;
    public static GameManager Instance
    {
        get 
        { 
            if (instance == null) 
            { 
                GameObject newGameObject = new GameObject("GameManager"); 
                instance = newGameObject.AddComponent<GameManager>(); 
            } 
            return instance; 
        } 
    }

    [Header("Game State")]
    [SerializeField] private bool isGameOver = false;
    [SerializeField] private int score = 0;
    [SerializeField] private int bonusScore = 0;
    [SerializeField] private int level = 1;
    [SerializeField] private bool[] bonusCollected = new bool[5];

    [Header("Level Settings")]
    [SerializeField] private int scorePerLevel = 100;
    [SerializeField] private int maxLevel = 4;

    public bool IsGameOver
    {
        get => isGameOver;
        set => isGameOver = value;
    }

    public int Score
    {
        get => score;
        set => score = Mathf.Max(0, value);
    }

    public int BonusScore
    {
        get => bonusScore;
        set => bonusScore = Mathf.Max(0, value);
    }
    public int Level => level;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
            return;
        }
        instance = this;

        DontDestroyOnLoad(this.gameObject);
    }

    public void UpdateLevel()
    {
        int computed = (score / scorePerLevel) + 1;
        level = Mathf.Clamp(computed, 1, maxLevel);
    }

    public void SetBonusCollected(int index)
    {
        if (index < 0 || index >= bonusCollected.Length) 
            return;

        bonusCollected[index] = true;
        GameEvents.RaiseBonusScoreAdded(index);
    }

    public void ResetBonusCollected()
    {
        for (int i = 0; i < bonusCollected.Length; i++)
            bonusCollected[i] = false;
    }

    public bool IsAllBonusCollected()
    {
        for (int i = 0; i < bonusCollected.Length; i++)
            if (!bonusCollected[i]) return false;
        return true;
    }

    public int GetTotalScore() => score + bonusScore;

    public void ResetGame()
    {
        score = 0;
        bonusScore = 0;
        isGameOver = false;
        level = 1;
        ResetBonusCollected();
    }

    public bool GetBonusCollected(int index)
    {
        if (index < 0 || index >= bonusCollected.Length) 
            return false;

        return bonusCollected[index];
    }
}