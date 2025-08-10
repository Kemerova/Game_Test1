using UnityEngine;

/// <summary>
/// Chases the player and deals contact damage via EnemyDamageDealer.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    public float moveSpeed = 2.5f;
    public int touchDamage = 10;
    public float touchCooldown = 0.6f;

    private Transform _player;
    private Rigidbody2D _rb;
    private float _touchTimer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.gravityScale = 0f;
        _rb.freezeRotation = true;
    }

    private void OnEnable()
    {
        _touchTimer = 0f;
        var p = GameObject.FindGameObjectWithTag("Player");
        _player = p ? p.transform : null;
    }

    private void FixedUpdate()
    {
        if (!_player) return;
        Vector2 dir = (_player.position - transform.position).normalized;
        _rb.velocity = dir * moveSpeed;
        _touchTimer -= Time.fixedDeltaTime;
    }

    private void OnCollisionStay2D(Collision2D col)
    {
        if (_touchTimer > 0f) return;
        if (col.collider.CompareTag("Player"))
        {
            var hp = col.collider.GetComponent<Health>();
            if (hp) hp.TakeDamage(touchDamage);
            _touchTimer = touchCooldown;
        }
    }
}