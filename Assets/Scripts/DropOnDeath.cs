using UnityEngine;

public class DropOnDeath : MonoBehaviour
{
    public ObjectPool xpPool;
    public float dropChance = 1.0f; // 100%
    public int minXP = 3;
    public int maxXP = 7;

    private Health _hp;

    private void Awake()
    {
        _hp = GetComponent<Health>();
        if (_hp) _hp.onDeath.AddListener(HandleDeath);
    }

    private void HandleDeath()
    {
        if (!xpPool) return;
        if (Random.value <= dropChance)
        {
            var gem = xpPool.Get(transform.position, Quaternion.identity);
            var pickup = gem.GetComponent<PickupXP>();
            if (pickup) pickup.xpValue = Random.Range(minXP, maxXP + 1);
        }
    }
}