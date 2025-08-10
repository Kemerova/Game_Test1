using UnityEngine;

[CreateAssetMenu(menuName = "Defs/Upgrade")]
public class UpgradeDef : ScriptableObject
{
    [Header("Display")]
    public string id;
    public string displayName;
    [TextArea] public string description;
    public Sprite icon;
    public Rarity rarity = Rarity.Common;

    [Header("Effect")]
    public UpgradeEffect effect;
    public float value = 0f;       // e.g., 0.15 for +15%, or +1 for flat increments
    public int maxStacks = 5;

    [Header("Offer Rules")]
    public bool unique = false;    // if true, only 1 stack total
}