using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    [Header("Spawn Area")]
    [SerializeField] private float minSpawnY = -3.0f;
    [SerializeField] private float maxSpawnY = 4.5f;

    [Header("Pooling")]
    [SerializeField] private int poolSize = 20;
    [SerializeField] private ItemController[] itemPrefabs;

    [Header("Timing")]
    [SerializeField] private float baseSpawnChance = 0.5f; // 0~1
    [SerializeField] private float spawnTime = 2.5f;
    [SerializeField] private float spawnTimeDecreasePerLevel = 0.2f;
    [SerializeField] private float spawnTimeClamp = 0.5f;

    private ItemController[] pool;
    private int currentIndex;

    private void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        pool = new ItemController[poolSize];
        for (int i = 0; i < poolSize; i++)
        {
            var prefab = itemPrefabs[i % itemPrefabs.Length];
            var instance = Instantiate(prefab, transform);
            instance.CurrentType = (EItemType)(i % itemPrefabs.Length);
            instance.gameObject.SetActive(false);
            pool[i] = instance;
        }
        pool.Shuffle();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Pipe"))
            TrySpawnItem();
    }

    private void TrySpawnItem()
    {
        UpdateLevelScaling();

        if (Random.value > baseSpawnChance)
            return;

        SpawnOneFromPool();
    }

    private void UpdateLevelScaling()
    {
        int level = GameManager.Instance.Level;
        spawnTime = Mathf.Max(spawnTimeClamp, 2.5f - spawnTimeDecreasePerLevel * (level - 1));
    }

    private void SpawnOneFromPool()
    {
        for (int i = 0; i < poolSize; i++)
        {
            currentIndex++;
            if (currentIndex >= poolSize)
            {
                pool.Shuffle();
                currentIndex %= poolSize;
            }   

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