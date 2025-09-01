using UnityEngine;

public class ItemController : MonoBehaviour
{
    public EItemType CurType { get; set; } = EItemType.B;
    private float speed = 8.0f;
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * (speed + 1.5f * (GameManager.Instance.level - 1)));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EndLine")
        {
            gameObject.SetActive(false);
        }
        else if(other.tag == "Player")
        {

            gameObject.SetActive(false);
        }
    }
}
