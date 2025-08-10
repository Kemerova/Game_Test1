using UnityEngine;

/// <summary>
/// Simple XP gem. On trigger with Player, awards XP and returns to pool.
/// </summary>
[RequireComponent(typeof(Collider2D))]
public class PickupXP : MonoBehaviour
{
    public int xpValue = 5;
    public float magnetRange = 0f; // set >0 later for auto-pull

    private Transform _player;
    private ObjectPool _pool;

    public void Init(ObjectPool pool) { _pool = pool; }

    private void OnEnable()
    {
        var p = GameObject.FindGameObjectWithTag("Player");
        _player = p ? p.transform : null;
    }

    private void Update()
    {
        if (magnetRange > 0f && _player)
        {
            float d = Vector2.Distance(_player.position, transform.position);
            if (d < magnetRange)
            {
                transform.position = Vector2.MoveTowards(transform.position, _player.position, 12f * Time.deltaTime);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) return;
        var xp = other.GetComponent<Experience>();
        if (xp) xp.AddXP(xpValue);
        if (_pool) _pool.Return(gameObject);
        else gameObject.SetActive(false);
    }
}