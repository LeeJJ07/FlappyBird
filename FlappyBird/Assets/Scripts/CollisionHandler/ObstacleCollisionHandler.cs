using UnityEngine;

public class ObstacleCollisionHandler : CollisionHandlerBase
{
    [SerializeField] private AudioClip deathClip;
    [SerializeField] private GameObject deathParticle;

    public override void HandleCollision(PlayerController player)
    {
        Instantiate(deathParticle, player.transform.position + new Vector3(1.5f, 0.0f, 0.0f), Quaternion.identity);
        GameManager.Instance.IsGameOver = true;
        PlaySfx(deathClip);
        player.gameObject.SetActive(false);
    }
}