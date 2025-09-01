using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource bgmSource;
    [SerializeField] private AudioSource sfxSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        if (bgmSource != null && !bgmSource.isPlaying)
            bgmSource.Play();
    }

    public void PlaySfx(AudioClip clip, float volume = 1f)
    {
        if (clip == null)
        {
            Debug.LogWarning("PlaySfx: AudioClip is null!");
            return;
        }

        if (sfxSource == null)
        {
            Debug.LogWarning("PlaySfx: sfxSource is null!");
            return;
        }

        Debug.Log($"PlaySfx: Playing {clip.name} at volume {volume}");
        sfxSource.PlayOneShot(clip, volume);
    }

}