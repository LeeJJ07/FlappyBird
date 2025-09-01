using System.Collections.Generic;
using UnityEngine;

public enum EItemType { B, O, N, U, S }
public class ItemSpawner : MonoBehaviour
{
    private readonly float maxSpawnY = 4.5f;
    private readonly float minSpawnY = -3.0f;
    private readonly int poolCapacity = 20;

    private float spawnTime = 2.5f;

    [SerializeField] private GameObject[] itemsPrefab;
    private List<GameObject> itemsPool = new List<GameObject>();

    private int generateIndex = 0;
    void Start()
    {
        SetPool();
    }


    private void SpawnItem()
    {
        int curLevel = GameManager.Instance.level;
        curLevel = SetLevel(curLevel);

        GenerateItem();
    }
    private int SetLevel(int curLevel)
    {
        if (curLevel == GameManager.Instance.level)
            return curLevel;

        curLevel = GameManager.Instance.level;
        spawnTime -= 0.2f * (curLevel - 1);

        return curLevel;
    }

    private void SetPool()
    {
        for(int num = 0; num < poolCapacity; num++)
        {
            //item 풀링 부분 미리 생성하는 부분에서 수정 필요
            GameObject item = Instantiate(itemsPrefab[num % itemsPrefab.Length]);
            item.transform.SetParent(transform, false);
            item.GetComponent<ItemController>().CurType = (EItemType)(num%itemsPrefab.Length);
            item.SetActive(false);

            itemsPool.Add(item);
        }
        itemsPool.Shuffle();
    }

    private void GenerateItem()
    {
        if (Random.Range(0, 2) == 0)
            return;
        while (true)
        {
            generateIndex++;
            if(generateIndex >= poolCapacity)
            {
                itemsPool.Shuffle();
                generateIndex %= poolCapacity;
            }

            if (itemsPool[generateIndex].activeSelf)
                continue;

            ResetPosition(generateIndex);
            itemsPool[generateIndex].SetActive(true);
            break;
        }
    }
    private void ResetPosition(int itemIndex)
    {
        itemsPool[itemIndex].transform.position =
            new Vector3(
                transform.position.x,
                Random.Range(minSpawnY, maxSpawnY),
                transform.position.z
            );
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Pipe")
            SpawnItem();
    }
}
