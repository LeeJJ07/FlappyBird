using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudController : MonoBehaviour
{
    private float speed = 6.0f;

    void Update()
    {
        transform.Translate(Vector3.left * Time.deltaTime * (speed + 2.0f * (GameManager.Instance.level - 1)));
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "EndLine")
        {
            gameObject.SetActive(false);
        }
    }
}
