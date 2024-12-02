using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameObject dieParticle;
    [SerializeField] private float jumpPower = 6.0f;

    private Rigidbody rigid;
    private bool onBuff = false;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        onBuff = false;
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
                if (onBuff)
                {
                    GameManager.Instance.score += 20;
                    GameManager.Instance.SetLevel();
                }
                else
                {
                    Instantiate(
                        dieParticle,
                        transform.position + new Vector3(1.5f, 0.0f, 0.0f),
                        Quaternion.identity
                        );
                    GameManager.Instance.isGameOver = true;
                    Destroy(gameObject);
                }
                break;
            case "ScoreBox":
                GameManager.Instance.score += 10;
                GameManager.Instance.SetLevel();
                break;
            case "Item":
                break;
        }
    }
}
