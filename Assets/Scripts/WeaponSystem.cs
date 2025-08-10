using UnityEngine;

/// <summary>
/// Auto-fires a simple projectile at nearest enemy within range.
/// Attach to Player. Requires pools & projectile prefab.
/// </summary>
public class WeaponSystem : MonoBehaviour
{
    [Header("Refs")]
    public ObjectPool projectilePool;

    [Header("Stats")]
    public float cooldown = 0.35f;
    public float range = 10f;
    public int damage = 10;
    public int pierce = 0;

    private float _timer;

    private void Update()
    {
        _timer -= Time.deltaTime;
        if (_timer > 0f) return;

        var target = FindNearestEnemy();
        if (target == null) return;

        Vector3 dir = (target.position - transform.position).normalized;
        Quaternion rot = Quaternion.LookRotation(Vector3.forward, Vector3.up); // keep z
        // Rotate so right points to target (since projectile uses transform.right)
        rot = Quaternion.FromToRotation(Vector3.right, dir);

        var projGO = projectilePool.Get(transform.position, rot);
        var proj = projGO.GetComponent<Projectile>();
        if (proj)
        {
            proj.damage = damage;
            proj.pierce = pierce;
            proj.sourceName = "Primary"; // rename per weapon type if you add more
            proj.Init(projectilePool);
        }

        _timer = cooldown;
    }

    private Transform FindNearestEnemy()
    {
        // Simple (and fine for small counts). Later: maintain a registry.
        float best = range * range;
        Transform bestT = null;
        foreach (var enemy in GameObject.FindGameObjectsWithTag("Enemy"))
        {
            float d2 = (enemy.transform.position - transform.position).sqrMagnitude;
            if (d2 < best)
            {
                best = d2;
                bestT = enemy.transform;
            }
        }
        return bestT;
    }
}