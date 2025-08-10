using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds the master list of upgrades offered by the draft.
/// Create one in the scene and populate in Inspector.
/// </summary>
public class UpgradeDatabase : MonoBehaviour
{
    public List<UpgradeDef> upgrades = new List<UpgradeDef>();

    public IEnumerable<UpgradeDef> All => upgrades;
}