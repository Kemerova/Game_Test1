using System;
using System.IO;
using System.Text;
using UnityEngine;

/// <summary>
/// Logs run metrics every interval to CSV files under persistentDataPath/logs/<run-id>.
/// Files:
///   summary.csv           => time_sec,kills,enemies_alive,total_damage
///   damage_by_source.csv  => time_sec,source,damage_cum,dps_cum
/// Designed for low overhead (once per interval).
/// </summary>
public class RuntimeCSVLogger : MonoBehaviour
{
    [Tooltip("Sample/append every N seconds (e.g., 60 for per-minute).")]
    public float intervalSeconds = 60f;

    [Tooltip("If true, also log on first frame (t≈0).")]
    public bool logAtStart = true;

    private string _dir;
    private string _summaryPath;
    private string _damagePath;
    private float _timer;
    private bool _inited;
    private float _lastLoggedAt;

    private void Awake()
    {
        // Unique folder per run
        var runId = DateTime.Now.ToString("yyyyMMdd_HHmmss");
        _dir = Path.Combine(Application.persistentDataPath, "logs", runId);
        Directory.CreateDirectory(_dir);
        _summaryPath = Path.Combine(_dir, "summary.csv");
        _damagePath = Path.Combine(_dir, "damage_by_source.csv");

        // Headers
        File.WriteAllText(_summaryPath, "time_sec,kills,enemies_alive,total_damage\n", Encoding.UTF8);
        File.WriteAllText(_damagePath, "time_sec,source,damage_cum,dps_cum\n", Encoding.UTF8);

        _inited = true;
        _timer = logAtStart ? 0f : intervalSeconds;
        _lastLoggedAt = 0f;

        Debug.Log($"[CSV] Logging to: {_dir}");
    }

    private void Update()
    {
        if (!_inited) return;
        _timer -= Time.unscaledDeltaTime;
        if (_timer > 0f) return;
        _timer = intervalSeconds;

        SampleAndWrite();
    }

    private void SampleAndWrite()
    {
        float t = StatsTracker.I ? StatsTracker.I.runTime : 0f;
        int kills = StatsTracker.I ? StatsTracker.I.enemiesKilled : 0;
        int enemiesAlive = CountAliveEnemies();
        float totalDamage = 0f;

        if (StatsTracker.I != null)
        {
            foreach (var kv in StatsTracker.I.DamageBySource)
                totalDamage += kv.Value;
        }

        // summary.csv row
        AppendCSV(_summaryPath, $"{t:0},{kills},{enemiesAlive},{totalDamage:0}\n");

        // damage_by_source.csv rows
        float dpsDiv = Mathf.Max(1f, t);
        if (StatsTracker.I != null)
        {
            foreach (var kv in StatsTracker.I.DamageBySource)
            {
                float dps = kv.Value / dpsDiv;
                AppendCSV(_damagePath, $"{t:0},{Sanitize(kv.Key)},{kv.Value:0},{dps:0.00}\n");
            }
        }

        _lastLoggedAt = t;
    }

    private static int CountAliveEnemies()
    {
        // Infrequent calls; OK to scan by tag.
        var arr = GameObject.FindGameObjectsWithTag("Enemy");
        int c = 0;
        for (int i = 0; i < arr.Length; i++)
            if (arr[i].activeInHierarchy) c++;
        return c;
    }

    private static string Sanitize(string s)
    {
        if (string.IsNullOrEmpty(s)) return "Unknown";
        s = s.Replace(",", "_").Replace("\n", " ").Replace("\r", " ");
        return s;
    }

    private static void AppendCSV(string path, string line)
    {
        try { File.AppendAllText(path, line, Encoding.UTF8); }
        catch (Exception e) { Debug.LogWarning($"[CSV] Write failed: {e.Message}"); }
    }
}