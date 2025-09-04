using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pressText;
    [SerializeField] private Image fadeImage;
    [SerializeField] private KeyCode startKey = KeyCode.Return;
    [SerializeField] private int gameSceneIndex = 1;

    private bool canStart = false;

    private void Start()
    {
        StartCoroutine(BlinkEffect());
    }

    private void Update()
    {
        if (!canStart && Input.GetKeyDown(startKey))
            StartCoroutine(FadeOut());
        if (canStart)
        {
            StopAllCoroutines();
            SceneManager.LoadScene(gameSceneIndex);
        }
    }

    private IEnumerator FadeOut()
    {
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime;
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, t);
            yield return null;
        }
        canStart = true;
    }

    private IEnumerator BlinkEffect()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.75f);
            var c = pressText.color;
            pressText.color = new Color(c.r, c.g, c.b, 0.0f);
            yield return new WaitForSeconds(0.4f);
            c = pressText.color;
            pressText.color = new Color(c.r, c.g, c.b, 1.0f);
        }
    }
}