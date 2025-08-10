using System;

[Serializable]
public class SaveData
{
    public float bestTimeSeconds = 0f;
    public int gamesPlayed = 0;

    public float masterVolume = 1f;
    public float musicVolume = 0.8f;
    public float sfxVolume = 1f;

    public int version = 1;
}