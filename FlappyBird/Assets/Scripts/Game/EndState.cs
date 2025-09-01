using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndState : MonoBehaviour, IState
{
    [SerializeField] private Canvas endingCanvas;
    [SerializeField] private GameObject renewEndingPanel;
    [SerializeField] private GameObject normalEndingPanel;
    [SerializeField] private GameObject buttonPanel;

    [SerializeField] private TextMeshProUGUI renewScoreText;

    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI curScoreText;

    [SerializeField] private Button goBackBtn;
    [SerializeField] private Button endGameBtn;
    private int highScore = 0;
    private void Start()
    {
        goBackBtn.onClick.AddListener(LoadStartScene);
        endGameBtn.onClick.AddListener(GameExit);
    }
    public void EnterState()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        if(highScore < GameManager.Instance.GetTotalScore())
        {
            highScore = GameManager.Instance.GetTotalScore();
            PlayerPrefs.SetInt("HighScore", highScore);

            renewScoreText.text = highScore.ToString() + "점";
            StartCoroutine(PopUpEnding(renewEndingPanel));
        }
        else
        {
            highScoreText.text = "최고 점수 " + highScore.ToString() + "점";
            curScoreText.text = "현재 점수 " + GameManager.Instance.GetTotalScore().ToString() + "점";

            StartCoroutine(PopUpEnding(normalEndingPanel));
        }

        StartCoroutine(PopUpButton());
    }
    public void UpdateState()
    {

    }
    public void ExitState()
    {

    }

    IEnumerator PopUpEnding(GameObject popUp)
    {
        popUp.transform.localScale = Vector3.zero;
        popUp.SetActive(true);

        float time = 0.0f;
        while(time < 1.0f)
        {
            time += Time.deltaTime;
            popUp.transform.localScale = Vector3.one * time;
            yield return null;
        }
    }

    IEnumerator PopUpButton()
    {
        yield return new WaitForSeconds(3.0f);
        buttonPanel.SetActive(true);
    }

    private void LoadStartScene()
    {
        GameManager.Instance.ResetGame();
        SceneManager.LoadScene(0);
    }
    private void GameExit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit(); // 어플리케이션 종료
#endif
    }
}
