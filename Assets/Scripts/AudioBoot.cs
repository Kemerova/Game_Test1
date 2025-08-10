using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// Applies saved mixer levels on boot (useful when the Settings menu isn't open yet).
/// </summary>
public class AudioBoot : MonoBehaviour
{
    public AudioMixer mixer;
    public string masterParam = "MasterVol";
    public string musicParam  = "MusicVol";
    public string sfxParam    = "SFXVol";

    private void Start()
    {
        var d = SaveSystem.I ? SaveSystem.I.Data : new SaveData();
        Apply(masterParam, d.masterVolume);
        Apply(musicParam,  d.musicVolume);
        Apply(sfxParam,    d.sfxVolume);
    }

    private void Apply(string param, float linear01)
    {
        float dB = (linear01 <= 0.0001f) ? -80f : Mathf.Log10(linear01) * 20f;
        if (mixer) mixer.SetFloat(param, dB);
    }
}