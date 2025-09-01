using UnityEngine;

public class PickUpParticle : MonoBehaviour
{
    [SerializeField] private float lifetime = 1.0f;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }
}