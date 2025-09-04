using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollisionHandlerBase : MonoBehaviour, ICollisionHandler
{
    public abstract void HandleCollision(PlayerController player);

    protected void PlaySfx(AudioClip clip, float volume = 1.0f)
    {
        GameEvents.RaisePlaySfx(clip, volume);
    }
}