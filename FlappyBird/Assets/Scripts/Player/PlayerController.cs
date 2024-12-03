using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject pickUpParticle;
    [SerializeField] private GameObject dieParticle;
    [SerializeField] private float jumpPower = 6.0f;

    [SerializeField] private GameObject hudScoreText;
    [SerializeField] private GameObject hudBonusScoreText;

    private Rigidbody rigid;
    private AudioSource audioSource;

    [Header("Sound Clips")]
    [SerializeField] AudioClip jumpClip;
    [SerializeField] AudioClip pickupClip;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveUp();
    }

    private void MoveUp()
    {
        if (Input.GetKeyDown(KeyCode.Space) && !rigid.isKinematic)
        {
            rigid.velocity = new Vector3(0.0f, jumpPower, 0.0f);
            audioSource.PlayOneShot(jumpClip);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case "Boundary":
                Instantiate(
                    dieParticle,
                    transform.position + new Vector3(1.5f, (transform.position.y > 0 ? 1.0f : -1.0f), 0.0f),
                    Quaternion.identity
                    );
                GameManager.Instance.isGameOver = true;
                Destroy(gameObject);
                break;
            case "Obstacle":
                Instantiate(
                    dieParticle,
                    transform.position + new Vector3(1.5f, 0.0f, 0.0f),
                    Quaternion.identity
                    );
                GameManager.Instance.isGameOver = true;
                Destroy(gameObject);
                break;
            case "ScoreBox": 
                GetScore();
                GameManager.Instance.score += 10;
                GameManager.Instance.SetLevel();
                audioSource.PlayOneShot(pickupClip);
                break;
            case "Item":
                GameManager.Instance.SetBonus((int)(other.gameObject.GetComponent<ItemController>().CurType));
                Instantiate(
                        pickUpParticle,
                        other.transform.position,
                        Quaternion.identity
                        );
                if (GameManager.Instance.CheckBonus())
                    StartCoroutine(GetBonusScore());
                audioSource.PlayOneShot(pickupClip);
                break;
        }
    }
    IEnumerator GetBonusScore()
    {
        GameObject hudText = Instantiate(hudBonusScoreText);
        GameManager.Instance.bonusScore += 300;
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.ResetBonus();
    }

    private void GetScore()
    {
        GameObject hudText = Instantiate(hudScoreText);
        hudText.transform.position = transform.position + new Vector3(2.5f, 2.0f, 0.0f);
    }
}
