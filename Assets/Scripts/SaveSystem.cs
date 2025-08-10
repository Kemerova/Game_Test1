using System.IO;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    public static SaveSystem I { get; private set; }
    public SaveData Data { get; private set; } = new SaveData();

    private string _path;

    private void Awake()
    {
        if (I && I != this) { Destroy(gameObject); return; }
        I = this;
        DontDestroyOnLoad(gameObject);
        _path = Path.Combine(Application.persistentDataPath, "survivorlite_save.json");
        Load();
    }

    public void Load()
    {
        try
        {
            if (File.Exists(_path))
            {
                var json = File.ReadAllText(_path);
                Data = JsonUtility.FromJson<SaveData>(json) ?? new SaveData();
            }
        }
        catch { Data = new SaveData(); }
    }

    public void Save()
    {
        try
        {
            var json = JsonUtility.ToJson(Data, prettyPrint: true);
            File.WriteAllText(_path, json);
        }
        catch { /* ignore disk errors for now */ }
    }

    public void RecordRun(float timeSec)
    {
        Data.gamesPlayed++;
        if (timeSec > Data.bestTimeSeconds) Data.bestTimeSeconds = timeSec;
        Save();
    }
}