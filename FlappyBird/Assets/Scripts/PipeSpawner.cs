using System.Collections;
using UnityEngine;

public class PipeSpawner : MonoBehaviour
{
    [Header("Spawn Area")]
    [SerializeField] private float minSpawnY = -4f;
    [SerializeField] private float maxSpawnY = 6f;

    [Header("Pooling")]
    [SerializeField] private int poolSize = 20;
    [SerializeField] private PipeController pipePrefab;

    [Header("Timing")]
    [SerializeField] private float minSpawnTime = 2.0f;
    [SerializeField] private float maxSpawnTime = 3.0f;
    [SerializeField] private float minTimeClamp = 0.3f;
    [SerializeField] private float maxTimeClamp = 0.5f;
    [SerializeField] private float minTimeDecreasePerLevel = 0.15f;
    [SerializeField] private float maxTimeDecreasePerLevel = 0.25f;

    private ObjectPool<PipeController> pool;
    private int cachedLevel;

    private void Start()
    {
        pool = new ObjectPool<PipeController>(pipePrefab, poolSize, transform);
        cachedLevel = GameManager.Instance.Level;
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            UpdateLevelScaling();
            yield return new WaitForSeconds(Random.Range(minSpawnTime, maxSpawnTime));
            SpawnOnce();
        }
    }

    private void UpdateLevelScaling()
    {
        int level = GameManager.Instance.Level;
        if (level == cachedLevel) return;

        int delta = level - 1;
        maxSpawnTime = Mathf.Max(maxTimeClamp, maxSpawnTime - maxTimeDecreasePerLevel * delta);
        minSpawnTime = Mathf.Max(minTimeClamp, minSpawnTime - minTimeDecreasePerLevel * delta);
        cachedLevel = level;
    }

    private void SpawnOnce()
    {
        var pipe = pool.Get();
        if (pipe == null) return;

        pipe.transform.position = new Vector3(
            transform.position.x,
            Random.Range(minSpawnY, maxSpawnY),
            transform.position.z
        );
        pipe.gameObject.SetActive(true);
    }
}