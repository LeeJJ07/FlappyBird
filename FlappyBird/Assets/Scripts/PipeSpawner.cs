using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    private readonly float maxSpawnY = 6.0f;
    private readonly float minSpawnY = -4.0f;
    private readonly int poolCapacity = 20;

    private float maxSpawnTime = 3.0f;
    private float minSpawnTime = 2.0f;
    [SerializeField] private GameObject pipePrefab;

    private List<GameObject> cloudsPool = new List<GameObject>();

    private int generateIndex = 0;
    void Start()
    {
        SetPool();

        StartCoroutine(SpawnPipe());
    }

    IEnumerator SpawnPipe()
    {
        int curLevel = GameManager.Instance.level;
        while (true)
        {
            curLevel = SetLevel(curLevel);
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));

            GeneratePipe();
        }
    }

    private int SetLevel(int curLevel)
    {
        if (curLevel == GameManager.Instance.level)
            return curLevel;

        curLevel = GameManager.Instance.level;
        maxSpawnTime -= 0.25f * (curLevel - 1);
        minSpawnTime -= 0.15f * (curLevel - 1);

        return curLevel;
    }
    private void SetPool()
    {
        for (int num = 0; num < poolCapacity; num++)
        {
            GameObject cloud = Instantiate(pipePrefab);
            cloud.transform.SetParent(transform, false);
            cloud.SetActive(false);

            cloudsPool.Add(cloud);
        }
    }
    private void GeneratePipe()
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
