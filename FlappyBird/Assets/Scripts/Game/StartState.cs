using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class StartState : MonoBehaviour, IState
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private GameObject player;
    [SerializeField] private float moveSpeed = 7.0f;
    [SerializeField] private Vector3 startPos = new Vector3(-21f, 0f, 0f);
    [SerializeField] private float readyXThreshold = -11.0f;

    private bool isFadeIn;

    public void EnterState()
    {
        player.transform.position = startPos;
        var rb = player.GetComponent<Rigidbody>();
        rb.isKinematic = true;
        isFadeIn = false;
        StartCoroutine(FadeIn());
    }

    public void UpdateState()
    {
        if (isFadeIn)
            player.transform.position += Vector3.right * Time.deltaTime * moveSpeed;
    }

    public void ExitState() { }

    public bool IsReadyToPlay()
    {
        if (player.transform.position.x > readyXThreshold)
        {
            player.GetComponent<Rigidbody>().isKinematic = false;
            return true;
        }
        return false;
    }

    private IEnumerator FadeIn()
    {
        float alpha = 1.0f;
        while (alpha > 0.0f)
        {
            alpha -= Time.deltaTime;
            fadeImage.color = new Color(0f, 0f, 0f, alpha);
            yield return null;
        }
        isFadeIn = true;
    }
}