using UnityEngine;

public class BoundaryCollisionHandler : CollisionHandlerBase
{
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private GameObject deathParticle;

    public override void HandleCollision(PlayerController player)
    {
        Vector3 offset = new Vector3(1.5f, player.transform.position.y > 0 ? 1.0f : -1.0f, 0.0f);
        Instantiate(deathParticle, player.transform.position + offset, Quaternion.identity);
        GameManager.Instance.IsGameOver = true;
        PlaySfx(deathClip);
        player.gameObject.SetActive(false);
    }
}