using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartState : MonoBehaviour, IState
{
    [SerializeField] private Image fadeImage;
    [SerializeField] private GameObject player;

    private float speed = 7.0f;

    private bool isFadeIn = false;
    public void EnterState()
    {
        player.transform.position = new Vector3(-21.0f, 0.0f, 0.0f);
        player.GetComponent<Rigidbody>().isKinematic = true;
        StartCoroutine(FadeIn());
    }
    public void UpdateState()
    {
        if(isFadeIn)
            player.transform.position += Vector3.right * Time.deltaTime * speed;
    }
    public void ExitState()
    {

    }

    public bool IsPossiblePlay()
    {
        if (player.transform.position.x > -11.0f)
        {
            player.GetComponent<Rigidbody>().isKinematic = false;
            return true;
        }
        return false;
    }
    IEnumerator FadeIn()
    {
        float alpha = 1.0f;

        while (alpha > 0.0f)
        {
            alpha -= Time.deltaTime;
            fadeImage.color = new Color(0.0f, 0.0f, 0.0f, alpha);

            yield return null;
        }
        isFadeIn = true;
    }
}
