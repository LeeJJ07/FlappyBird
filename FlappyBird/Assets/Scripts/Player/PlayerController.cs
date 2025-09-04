using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float jumpPower = 6.0f;
    [SerializeField] private AudioClip jumpClip;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !rb.isKinematic)
        {
            rb.velocity = new Vector3(0.0f, jumpPower, 0.0f);
            GameEvents.RaisePlaySfx(jumpClip);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        var handler = other.GetComponent<ICollisionHandler>();
        handler?.HandleCollision(this);
    }
}