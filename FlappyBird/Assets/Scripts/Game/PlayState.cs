using TMPro;
using UnityEngine;

public class PlayState : MonoBehaviour, IState
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;

    [SerializeField] private TextMeshProUGUI[] bonusText;
    public void EnterState()
    {
        canvas.enabled = true;
    }
    public void UpdateState()
    {
        scoreText.text = "현재 점수 : " + GameManager.Instance.GetTotalScore().ToString(); 
        levelText.text = "Level " + GameManager.Instance.level.ToString();

        for(int num = 0; num < bonusText.Length; num++)
        {
            if (GameManager.Instance.isBonus[num])
                bonusText[num].enabled = true;
            else
                bonusText[num].enabled = false;
        }
    }
    public void ExitState()
    {
        canvas.enabled = false;
    }
}
