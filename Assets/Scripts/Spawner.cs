using UnityEngine;

/// <summary>
/// Spawns enemies just off-screen in a ring around camera.
/// </summary>
public class Spawner : MonoBehaviour
{
    public ObjectPool enemyPool;
    public float spawnInterval = 1.0f;
    public float ringRadius = 14f;
    public Transform player;
    public Camera worldCamera;

    private float _timer;

    private void Reset()
    {
        worldCamera = Camera.main;
    }

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer > 0f) return;

        if (!player) return;
        Vector2 spawnPos = RandomRingPosition();
        var enemy = enemyPool.Get(spawnPos, Quaternion.identity);
        _timer = spawnInterval;
    }

    private Vector2 RandomRingPosition()
    {
        // place outside camera view roughly in a circle
        Vector2 center = player ? (Vector2)player.position : Vector2.zero;
        float angle = Random.Range(0f, Mathf.PI * 2f);
        var pos = center + new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * ringRadius;
        return pos;
    }
}