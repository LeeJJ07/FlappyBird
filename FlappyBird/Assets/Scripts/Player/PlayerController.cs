using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Particles")]
    [SerializeField] private GameObject pickUpParticle;
    [SerializeField] private GameObject deathParticle;

    [Header("Movement")]
    [SerializeField] private float jumpPower = 6.0f;

    [Header("HUD Prefabs")]
    [SerializeField] private GameObject scoreHudTextPrefab;
    [SerializeField] private GameObject bonusHudTextPrefab;
    [SerializeField] private Vector3 scoreHudOffset = new Vector3(2.5f, 2.0f, 0f);

    [Header("Audio")]
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip pickupClip;
    [SerializeField] private AudioClip deathClip;

    private Rigidbody _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        HandleJump();
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !_rb.isKinematic)
        {
            _rb.velocity = new Vector3(0f, jumpPower, 0f);
            SoundManager.Instance?.PlaySfx(jumpClip);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Boundary"))
        {
            SpawnDeathEffect(new Vector3(1.5f, transform.position.y > 0 ? 1.0f : -1.0f, 0f));
            GameManager.Instance.IsGameOver = true;
            SoundManager.Instance?.PlaySfx(deathClip);
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("Obstacle"))
        {
            SpawnDeathEffect(new Vector3(1.5f, 0f, 0f));
            GameManager.Instance.IsGameOver = true;
            SoundManager.Instance?.PlaySfx(deathClip);
            gameObject.SetActive(false);
        }
        else if (other.CompareTag("ScoreBox"))
        {
            AddScore(10);
            SoundManager.Instance?.PlaySfx(pickupClip);
        }
        else if (other.CompareTag("Item"))
        {
            var item = other.GetComponent<ItemController>();
            if (item != null)
            {
                GameManager.Instance.SetBonusCollected((int)item.CurrentType);
                Instantiate(pickUpParticle, other.transform.position, Quaternion.identity);
                if (GameManager.Instance.IsAllBonusCollected())
                    StartCoroutine(ApplyBonusScore(300));
                SoundManager.Instance?.PlaySfx(pickupClip);
            }
        }
    }

    private void SpawnDeathEffect(Vector3 offset)
    {
        Instantiate(deathParticle, transform.position + offset, Quaternion.identity);
    }

    private void AddScore(int value)
    {
        GameManager.Instance.Score += value;
        GameManager.Instance.UpdateLevel();

        if (scoreHudTextPrefab != null)
        {
            var hudText = Instantiate(scoreHudTextPrefab);
            hudText.transform.position = transform.position + scoreHudOffset;
        }
    }

    private IEnumerator ApplyBonusScore(int bonusValue)
    {
        GameManager.Instance.BonusScore += bonusValue;
        if (bonusHudTextPrefab != null)
            Instantiate(bonusHudTextPrefab);

        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.ResetBonusCollected();
    }
}