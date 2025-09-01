using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndState : MonoBehaviour, IState
{
    [SerializeField] private Canvas endingCanvas;
    [SerializeField] private GameObject newRecordPanel;
    [SerializeField] private GameObject normalPanel;
    [SerializeField] private GameObject buttonPanel;

    [Header("Texts")]
    [SerializeField] private TextMeshProUGUI newRecordScoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI currentScoreText;

    [Header("Buttons")]
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    [Header("Formats")]
    [SerializeField] private string newRecordFormat = "{0} pts";
    [SerializeField] private string highScoreFormat = "High score: {0} pts";
    [SerializeField] private string currentScoreFormat = "Current score: {0} pts";

    [Header("Timing")]
    [SerializeField] private float popupDuration = 1.0f;
    [SerializeField] private float buttonDelay = 3.0f;

    private int highScore;

    private void Start()
    {
        if (restartButton != null) restartButton.onClick.AddListener(LoadStartScene);
        if (quitButton != null) quitButton.onClick.AddListener(QuitGame);
    }

    public void EnterState()
    {
        if (endingCanvas != null) endingCanvas.enabled = true;

        highScore = PlayerPrefs.GetInt("HighScore", 0);
        int total = GameManager.Instance.GetTotalScore();

        if (highScore < total)
        {
            highScore = total;
            PlayerPrefs.SetInt("HighScore", highScore);

            if (newRecordScoreText != null)
                newRecordScoreText.text = string.Format(newRecordFormat, highScore);

            StartCoroutine(PopUpPanel(newRecordPanel));
        }
        else
        {
            if (highScoreText != null)
                highScoreText.text = string.Format(highScoreFormat, highScore);
            if (currentScoreText != null)
                currentScoreText.text = string.Format(currentScoreFormat, total);

            StartCoroutine(PopUpPanel(normalPanel));
        }

        StartCoroutine(ShowButtonsDelayed());
    }

    public void UpdateState() { }
    public void ExitState() { }

    private IEnumerator PopUpPanel(GameObject panel)
    {
        if (panel == null) yield break;

        panel.transform.localScale = Vector3.zero;
        panel.SetActive(true);

        float t = 0f;
        while (t < popupDuration)
        {
            t += Time.deltaTime;
            float s = Mathf.Clamp01(t / popupDuration);
            panel.transform.localScale = Vector3.one * s;
            yield return null;
        }
        panel.transform.localScale = Vector3.one;
    }

    private IEnumerator ShowButtonsDelayed()
    {
        yield return new WaitForSeconds(buttonDelay);
        if (buttonPanel != null)
            buttonPanel.SetActive(true);
    }

    private void LoadStartScene()
    {
        GameManager.Instance.ResetGame();
        SceneManager.LoadScene(0);
    }

    private void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}