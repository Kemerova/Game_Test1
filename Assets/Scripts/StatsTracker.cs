using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Tracks run stats (kills, time, per-weapon damage) for the summary.
/// Access with StatsTracker.I from anywhere.
/// </summary>
public class StatsTracker : MonoBehaviour
{
    public static StatsTracker I { get; private set; }

    public float runTime;
    public int enemiesKilled;

    // damageBySource: e.g., "MagicMissile" -> total damage dealt this run
    private readonly Dictionary<string, float> _damageBySource = new();

    private void Awake()
    {
        if (I && I != this) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);
        ResetRun();
    }

    public void ResetRun()
    {
        runTime = 0f;
        enemiesKilled = 0;
        _damageBySource.Clear();
    }

    private void Update()
    {
        if (Time.timeScale > 0f)
            runTime += Time.unscaledDeltaTime;
    }

    public void AddDamage(string source, float amount)
    {
        if (string.IsNullOrEmpty(source)) source = "Unknown";
        _damageBySource.TryGetValue(source, out var v);
        _damageBySource[source] = v + Mathf.Max(0f, amount);
    }

    public void AddKill() => enemiesKilled++;

    public IReadOnlyDictionary<string, float> DamageBySource => _damageBySource;
}