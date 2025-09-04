using System;
using UnityEngine;

public static class GameEvents
{
    public static event Action<int> OnScoreAdded;
    public static event Action<int> OnBonusScoreAdded;
    public static event Action<Vector3> OnPlayerDeath;
    public static event Action<AudioClip, float> OnPlaySfx;

    public static void RaiseScoreAdded(int score) => OnScoreAdded?.Invoke(score);
    public static void RaiseBonusScoreAdded(int score) => OnBonusScoreAdded?.Invoke(score);
    public static void RaisePlayerDeath(Vector3 pos) => OnPlayerDeath?.Invoke(pos);
    public static void RaisePlaySfx(AudioClip clip, float volume = 1.0f) => OnPlaySfx?.Invoke(clip, volume);
}