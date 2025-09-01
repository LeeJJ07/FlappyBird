using System.Collections;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    [Header("Spawn Area")]
    [SerializeField] private float minSpawnY = -9.0f;
    [SerializeField] private float maxSpawnY = 11.0f;

    [Header("Pooling")]
    [SerializeField] private int poolSize = 15;
    [SerializeField] private CloudController[] cloudPrefabs;

    [Header("Timing")]
    [SerializeField] private float minSpawnTime = 1.5f;
    [SerializeField] private float maxSpawnTime = 3.0f;

    private CloudController[] pool;
    private int currentIndex;

    private void Start()
    {
        InitializePool();
        StartCoroutine(SpawnRoutine());
    }

    private void InitializePool()
    {
        pool = new CloudController[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            var prefab = cloudPrefabs[Random.Range(0, cloudPrefabs.Length)];
            var cloud = Instantiate(prefab, transform);
            cloud.gameObject.SetActive(false);
            pool[i] = cloud;
        }
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            SpawnOnce();
        }
    }

    private void SpawnOnce()
    {
        for (int i = 0; i < poolSize; i++)
        {
            currentIndex = (currentIndex + 1) % poolSize;
            if (!pool[currentIndex].gameObject.activeSelf)
            {
                ResetPosition(pool[currentIndex].transform);
                pool[currentIndex].gameObject.SetActive(true);
                break;
            }
        }
    }

    private void ResetPosition(Transform t)
    {
        t.position = new Vector3(
            transform.position.x,
            Random.Range(minSpawnY, maxSpawnY),
            transform.position.z
        );
    }
}