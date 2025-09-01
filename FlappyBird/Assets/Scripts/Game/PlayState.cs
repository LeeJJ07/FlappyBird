using TMPro;
using UnityEngine;

public class PlayState : MonoBehaviour, IState
{
    [SerializeField] private Canvas hudCanvas;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI[] bonusTextIndicators;

    [Header("UI Formats")]
    [SerializeField] private string scoreFormat = "Score: {0}";
    [SerializeField] private string levelFormat = "Level {0}";

    public void EnterState()
    {
        if (hudCanvas != null) hudCanvas.enabled = true;
    }

    public void UpdateState()
    {
        if (scoreText != null)
            scoreText.text = string.Format(scoreFormat, GameManager.Instance.GetTotalScore());

        if (levelText != null)
            levelText.text = string.Format(levelFormat, GameManager.Instance.Level);

        for (int i = 0; i < bonusTextIndicators.Length; i++)
        {
            bool on = GameManager.Instance.GetBonusCollected(i);
            bonusTextIndicators[i].enabled = on;
        }
    }

    public void ExitState()
    {
        if (hudCanvas != null) hudCanvas.enabled = false;
    }
}