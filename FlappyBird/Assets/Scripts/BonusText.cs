using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BonusText : MonoBehaviour
{
    private float moveSpeed = 2.0f;
    private float alphaSpeed = 2.0f;
    private float destroyTime = 2.0f;

    private TextMeshPro text;
    private Color alpha;

    void Start()
    {
        text = GetComponent<TextMeshPro>();
        text.text = "+300";
        alpha = text.color;
        Destroy(gameObject, destroyTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0.0f, moveSpeed * Time.deltaTime, 0.0f));
        alpha.a = Mathf.Lerp(alpha.a, 0.0f, Time.deltaTime * alphaSpeed);
        text.color = alpha;
    }
}
