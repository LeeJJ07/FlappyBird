using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeController : MonoBehaviour
{
    private float speed = 8.0f;
    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * (speed + 1.5f * (GameManager.Instance.level - 1)));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "EndLine")
        {
            gameObject.SetActive(false);
        }
    }
}
