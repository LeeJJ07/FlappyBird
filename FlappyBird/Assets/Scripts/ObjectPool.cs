using System.Collections.Generic;
using UnityEngine;
public class ObjectPool<T> where T : Component
{
    private readonly List<T> pool = new List<T>();
    private readonly T prefab;
    private readonly Transform parent;

    public ObjectPool(T prefab, int capacity, Transform parent)
    {
        this.prefab = prefab;
        this.parent = parent;

        for (int i = 0; i < capacity; i++)
        {
            var obj = GameObject.Instantiate(this.prefab, this.parent);
            obj.gameObject.SetActive(false);
            this.pool.Add(obj);
        }
    }

    public T Get()
    {
        for (int i = 0; i < this.pool.Count; i++)
        {
            if (!this.pool[i].gameObject.activeSelf)
                return this.pool[i];
        }
        return null;
    }

    public IEnumerable<T> All => this.pool;
}