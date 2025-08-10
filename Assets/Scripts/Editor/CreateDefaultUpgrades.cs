#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;
using System.IO;

public class CreateDefaultUpgrades
{
    [MenuItem("SurvivorLite/Generate Default Upgrades")]
    public static void Generate()
    {
        string dir = "Assets/ScriptableObjects/Upgrades";
        if (!AssetDatabase.IsValidFolder("Assets/ScriptableObjects"))
            AssetDatabase.CreateFolder("Assets", "ScriptableObjects");
        if (!AssetDatabase.IsValidFolder(dir))
            AssetDatabase.CreateFolder("Assets/ScriptableObjects", "Upgrades");

        void Make(string id, string name, Rarity rar, UpgradeEffect eff, float val, int max, bool unique=false, string desc=null)
        {
            var so = ScriptableObject.CreateInstance<UpgradeDef>();
            so.id = id;
            so.displayName = name;
            so.rarity = rar;
            so.effect = eff;
            so.value = val;
            so.maxStacks = max;
            so.unique = unique;
            so.description = string.IsNullOrEmpty(desc) ? $"{eff} {(val>0?"+":"")}{val}" : desc;

            string path = $"{dir}/{id}.asset";
            AssetDatabase.CreateAsset(so, path);
        }

        // 12 ready-to-use upgrades
        Make("power_shot", "Power Shot", Rarity.Common, UpgradeEffect.DamagePct, 0.15f, 5, false, "+15% damage");
        Make("quick_hands", "Quick Hands", Rarity.Common, UpgradeEffect.CooldownPct, -0.10f, 5, false, "-10% cooldown");
        Make("fleet_footed", "Fleet-Footed", Rarity.Common, UpgradeEffect.MoveSpeedPct, 0.10f, 5, false, "+10% move speed");
        Make("iron_constitution", "Iron Constitution", Rarity.Rare, UpgradeEffect.MaxHPFlat, 20f, 3, false, "+20 Max HP");
        Make("vacuum_trinket", "Vacuum Trinket", Rarity.Epic, UpgradeEffect.MagnetRangeFlat, 3f, 2, false, "+3 magnet range");
        Make("arcane_circle", "Arcane Circle", Rarity.Epic, UpgradeEffect.AreaPct, 0.20f, 3, false, "+20% area");
        Make("multishot", "Multishot", Rarity.Rare, UpgradeEffect.ProjectileCountPlus, 1f, 3, false, "+1 projectile");
        Make("sharpened_tips", "Sharpened Tips", Rarity.Rare, UpgradeEffect.PiercePlus, 1f, 3, false, "+1 pierce");
        Make("chromatic_mastery", "Chromatic Mastery", Rarity.Legendary, UpgradeEffect.DamagePct, 0.35f, 1, true, "+35% damage (Unique)");
        Make("rapid_reload", "Rapid Reload", Rarity.Common, UpgradeEffect.CooldownPct, -0.07f, 5, false, "-7% cooldown");
        Make("mighty_bulwark", "Mighty Bulwark", Rarity.Rare, UpgradeEffect.MaxHPFlat, 35f, 2, false, "+35 Max HP");
        Make("hyper_focus", "Hyper Focus", Rarity.Epic, UpgradeEffect.DamagePct, 0.25f, 2, false, "+25% damage");

        AssetDatabase.SaveAssets();
        EditorUtility.DisplayDialog("SurvivorLite", "Default Upgrades generated.\nAdd them to your UpgradeDatabase list.", "OK");
    }
}
#endif