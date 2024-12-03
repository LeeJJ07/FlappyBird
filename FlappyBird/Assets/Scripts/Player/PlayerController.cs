using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject pickUpParticle;
    [SerializeField] private GameObject dieParticle;
    [SerializeField] private float jumpPower = 6.0f;

    private Rigidbody rigid;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveUp();
    }

    private void MoveUp()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            rigid.velocity = new Vector3(0.0f, jumpPower, 0.0f);
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
                GameManager.Instance.score += 10;
                GameManager.Instance.SetLevel();
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
                break;
        }
    }
    IEnumerator GetBonusScore()
    {
        yield return new WaitForSeconds(0.5f);
        GameManager.Instance.ResetBonus();
    }
}
