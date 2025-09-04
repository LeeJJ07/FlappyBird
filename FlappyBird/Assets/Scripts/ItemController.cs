using UnityEngine;

public enum EItemType { B, O, N, U, S }

public class ItemController : MonoBehaviour
{
    public EItemType CurrentType { get; set; } = EItemType.B;

    [SerializeField] private float baseSpeed = 8.0f;
    [SerializeField] private float speedIncreasePerLevel = 1.5f;

    private void Update()
    {
        float speed = baseSpeed + speedIncreasePerLevel * (GameManager.Instance.Level - 1);
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("EndLine") || other.CompareTag("Player"))
            gameObject.SetActive(false);
    }
}