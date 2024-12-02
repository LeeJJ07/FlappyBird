using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    private readonly float maxSpawnY = 11.0f;
    private readonly float minSpawnY = -9.0f;
    private readonly int poolCapacity = 15;

    private readonly float maxSpawnTime = 3.0f;
    private readonly float minSpawnTime = 1.5f;
    [SerializeField] private GameObject[] cloudPrefabs;

    private List<GameObject> cloudsPool = new List<GameObject>();

    private int generateIndex = 0;
    void Start()
    {
        SetPool();

        StartCoroutine(SpawnCloud());
    }

    IEnumerator SpawnCloud()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

            GenerateCloud();
        }
    }

    private void SetPool()
    {
        for (int num = 0; num < poolCapacity; num++)
        {
            GameObject cloud = Instantiate(cloudPrefabs[Random.Range(0, cloudPrefabs.Length)]);
            cloud.transform.SetParent(transform, false);
            cloud.SetActive(false);

            cloudsPool.Add(cloud);
        }
    }
    private void GenerateCloud()
    {
        while (true)
        {
            generateIndex++;
            generateIndex %= poolCapacity;

            if (cloudsPool[generateIndex].activeSelf)
                continue;

            ResetPosition(generateIndex);
            cloudsPool[generateIndex].SetActive(true);
            break;
        }
    }
    private void ResetPosition(int cloudIndex)
    {
        cloudsPool[cloudIndex].transform.position =
            new Vector3(
                transform.position.x,
                Random.Range(minSpawnY, maxSpawnY),
                transform.position.z
            );  
    }
}
