using System.Collections;
using UnityEngine;

public class ItemCollisionHandler : CollisionHandlerBase
{
    [SerializeField] private GameObject pickUpParticle;
    [SerializeField] private GameObject bonusHudTextPrefab;
    [SerializeField] private AudioClip pickupClip;
    [SerializeField] private int bonusScoreValue = 300;

    public override void HandleCollision(PlayerController player)
    {
        var item = GetComponent<ItemController>();
        if (item == null) return;

        GameManager.Instance.SetBonusCollected((int)item.CurrentType);
        Instantiate(pickUpParticle, transform.position, Quaternion.identity);
        PlaySfx(pickupClip);

        if (GameManager.Instance.IsAllBonusCollected())
            player.StartCoroutine(ApplyBonusScore());
    }


    private IEnumerator ApplyBonusScore()
    {
        GameManager.Instance.BonusScore += bonusScoreValue;
        GameEvents.RaiseBonusScoreAdded(bonusScoreValue);

        if (bonusHudTextPrefab != null)
            Instantiate(bonusHudTextPrefab);

        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.ResetBonusCollected();
    }
}