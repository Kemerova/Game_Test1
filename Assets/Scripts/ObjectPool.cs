using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple pool for GameObjects (projectiles, enemies, pickups).
/// </summary>
public class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    public int initialSize = 16;

    private readonly Queue<GameObject> _pool = new Queue<GameObject>();

    private void Awake()
    {
        for (int i = 0; i < Mathf.Max(1, initialSize); i++)
        {
            var go = Instantiate(prefab, transform);
            go.SetActive(false);
            _pool.Enqueue(go);
        }
    }

    public GameObject Get(Vector3 pos, Quaternion rot)
    {
        GameObject go = _pool.Count > 0 ? _pool.Dequeue() : Instantiate(prefab, transform);
        go.transform.SetPositionAndRotation(pos, rot);
        go.SetActive(true);
        return go;
    }

    public void Return(GameObject go)
    {
        go.SetActive(false);
        _pool.Enqueue(go);
    }
}