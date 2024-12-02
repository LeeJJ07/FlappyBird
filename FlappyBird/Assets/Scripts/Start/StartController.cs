using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pressText;
    [SerializeField] private Image fadeImage;

    private bool canStart = false;
    void Start()
    {
        StartCoroutine(BlinkEffect());
    }

    void Update()
    {
        LoadNextScene();
    }
    private void LoadNextScene()
    {
        if (!canStart && Input.GetKeyDown(KeyCode.Return))
            StartCoroutine(FadeOut());
        if (canStart)
        {
            StopAllCoroutines();
            SceneManager.LoadScene(1);
        }
    }
    IEnumerator FadeOut()
    {
        float time = 0.0f;
        while (time < 1.0f)
        {
            time += Time.deltaTime;
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, time);

            yield return null;
        }
        canStart = true;
    }

    IEnumerator BlinkEffect()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.75f);
            pressText.color = new Color(pressText.color.r, pressText.color.g, pressText.color.b, 0.0f);
            yield return new WaitForSeconds(0.4f);
            pressText.color = new Color(pressText.color.r, pressText.color.g, pressText.color.b, 1.0f);
        }
    }
}
