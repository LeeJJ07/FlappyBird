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
    private void OnEnable() => GameEvents.OnPlaySfx += PlaySfx;
    private void OnDisable() => GameEvents.OnPlaySfx -= PlaySfx;


    public void PlaySfx(AudioClip clip, float volume = 1.0f)
    {
        if (clip == null || sfxSource == null)
            return;

        sfxSource.PlayOneShot(clip, volume);
    }
}