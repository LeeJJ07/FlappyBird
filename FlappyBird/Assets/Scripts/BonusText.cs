using TMPro;
using UnityEngine;

public class BonusText : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2.0f;
    [SerializeField] private float fadeSpeed = 2.0f;
    [SerializeField] private float lifetime = 2.0f;
    [SerializeField] private string displayText = "+300";

    private TextMeshPro tmp;
    private Color color;

    private void Start()
    {
        tmp = GetComponent<TextMeshPro>();
        tmp.text = displayText;
        color = tmp.color;
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
        color.a = Mathf.Lerp(color.a, 0f, Time.deltaTime * fadeSpeed);
        tmp.color = color;
    }
}