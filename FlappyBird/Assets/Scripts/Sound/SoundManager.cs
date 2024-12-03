using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource bgm;

    private void Awake()
    {
        var soundManagers = FindObjectsOfType<SoundManager>();
        if (soundManagers.Length == 1)
            DontDestroyOnLoad(gameObject);
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        bgm.Play();
    }
}
