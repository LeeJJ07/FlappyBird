using TMPro;
using UnityEngine;

public class PlayState : MonoBehaviour, IState
{
    [SerializeField] private Canvas hudCanvas;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI levelText;
    [SerializeField] private TextMeshProUGUI[] bonusTextIndicators;

    private void OnEnable()
    {
        GameEvents.OnScoreAdded += OnScoreChanged;
        GameEvents.OnBonusScoreAdded += OnScoreChanged;
    }

    private void OnDisable()
    {
        GameEvents.OnScoreAdded -= OnScoreChanged;
        GameEvents.OnBonusScoreAdded -= OnScoreChanged;
    }


    private void OnScoreChanged(int _)
    {
        UpdateUI();
    }

    public void EnterState()
    {
        hudCanvas.enabled = true;
        UpdateUI();
    }

    public void UpdateState() { }
    public void ExitState() => hudCanvas.enabled = false;

    private void UpdateUI()
    {
        scoreText.text = $"Score: {GameManager.Instance.GetTotalScore()}";
        levelText.text = $"Level {GameManager.Instance.Level}";

        for (int i = 0; i < bonusTextIndicators.Length; i++)
            bonusTextIndicators[i].enabled = GameManager.Instance.GetBonusCollected(i);
    }
}