using UnityEngine;

/// <summary>
/// Moves forward, damages enemies on trigger, returns to pool on hit or timeout.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 10;
    public float lifetime = 3f;
    public int pierce = 0;
    public string sourceName = "Default"; // set by WeaponSystem

    private float _t;
    private ObjectPool _pool;
    private int _hits;

    public void Init(ObjectPool pool) { _pool = pool; }

    private void OnEnable()
    {
        _t = 0f; _hits = 0;
    }

    private void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
        _t += Time.deltaTime;
        if (_t >= lifetime) Despawn();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Enemy")) return;

        var hp = other.GetComponent<Health>();
        if (hp)
        {
            hp.TakeDamage(damage);
            StatsTracker.I?.AddDamage(sourceName, damage);
            if (hp.currentHP <= 0) StatsTracker.I?.AddKill();
        }

        _hits++;
        if (_hits > pierce) Despawn();
    }

    private void Despawn()
    {
        if (_pool) _pool.Return(gameObject);
        else gameObject.SetActive(false);
    }
}