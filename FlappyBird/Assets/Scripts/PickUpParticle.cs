using UnityEngine;

public class PickUpParticle : MonoBehaviour
{
    void Start()
    {
        Destroy(gameObject, 1.0f);
    }
}
