using UnityEngine;

public class PipeController : MonoBehaviour
{
    [SerializeField] private float baseSpeed = 8.0f;
    [SerializeField] private float speedIncreasePerLevel = 1.5f;

    private void Update()
    {
        float speed = baseSpeed + speedIncreasePerLevel * (GameManager.Instance.Level - 1);
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndLine"))
            gameObject.SetActive(false);
    }
}