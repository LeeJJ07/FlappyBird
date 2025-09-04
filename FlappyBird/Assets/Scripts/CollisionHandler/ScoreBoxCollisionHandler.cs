using UnityEngine;

public class ScoreBoxCollisionHandler : CollisionHandlerBase
{
    [SerializeField] private int scoreValue = 10;
    [SerializeField] private GameObject scoreHudTextPrefab;
    [SerializeField] private Vector3 hudOffset = new Vector3(2.5f, 2.0f, 0.0f);
    [SerializeField] private AudioClip pickupClip;

    public override void HandleCollision(PlayerController player)
    {
        GameManager.Instance.Score += scoreValue;
        GameManager.Instance.UpdateLevel();
        GameEvents.RaiseScoreAdded(scoreValue);
        PlaySfx(pickupClip);

        if (scoreHudTextPrefab != null)
        {
            var hudText = Instantiate(scoreHudTextPrefab);
            hudText.transform.position = player.transform.position + hudOffset;
        }
    }
}