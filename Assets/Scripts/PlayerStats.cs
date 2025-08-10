using UnityEngine;

/// <summary>
/// Centralized player stats with additive/multiplicative mods.
/// Other systems (WeaponSystem, movement, pickups) read from here.
/// </summary>
public class PlayerStats : MonoBehaviour
{
    [Header("Base Stats")]
    public int baseMaxHP = 100;
    public float baseMoveSpeed = 6f;
    public float baseMagnetRange = 0f;

    [Header("Weapon Global Mods")]
    public float damagePct = 0f;       // +0.25 = +25% dmg
    public float cooldownPct = 0f;     // -0.15 = -15% cooldown
    public int projectileCountPlus = 0;
    public int piercePlus = 0;
    public float areaPct = 0f;

    // Runtime computed properties
    public int MaxHP => baseMaxHP + _maxHPFlat;
    public float MoveSpeed => baseMoveSpeed * (1f + _moveSpeedPct);
    public float MagnetRange => baseMagnetRange + _magnetRangeFlat;

    private int _maxHPFlat = 0;
    private float _moveSpeedPct = 0f;
    private float _magnetRangeFlat = 0f;

    public void Apply(UpgradeEffect effect, float value)
    {
        switch (effect)
        {
            case UpgradeEffect.MaxHPFlat:        _maxHPFlat += Mathf.RoundToInt(value); break;
            case UpgradeEffect.MoveSpeedPct:     _moveSpeedPct += value; break;
            case UpgradeEffect.MagnetRangeFlat:  _magnetRangeFlat += value; break;

            case UpgradeEffect.DamagePct:        damagePct += value; break;
            case UpgradeEffect.CooldownPct:      cooldownPct += value; break;
            case UpgradeEffect.ProjectileCountPlus: projectileCountPlus += Mathf.RoundToInt(value); break;
            case UpgradeEffect.PiercePlus:       piercePlus += Mathf.RoundToInt(value); break;
            case UpgradeEffect.AreaPct:          areaPct += value; break;
        }
    }
}