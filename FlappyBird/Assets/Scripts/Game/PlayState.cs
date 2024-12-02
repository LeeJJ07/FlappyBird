using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayState : MonoBehaviour, IState
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;
    public void EnterState()
    {
        canvas.enabled = true;
    }
    public void UpdateState()
    {
        scoreText.text = "현재 점수 : " + GameManager.Instance.score.ToString(); 
        levelText.text = "Level " + GameManager.Instance.level.ToString(); 
    }
    public void ExitState()
    {
        canvas.enabled = false;
    }
}
