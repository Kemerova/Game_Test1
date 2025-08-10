using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

/// <summary>
/// Binds UI sliders to an AudioMixer and persists values via SaveSystem.
/// Expects exposed mixer params: "MasterVol", "MusicVol", "SFXVol".
/// Slider range should be 0..1 (linear). Converts to dB when applying.
/// </summary>
public class SettingsMenu : MonoBehaviour
{
    [Header("Mixer")]
    public AudioMixer mixer;
    public string masterParam = "MasterVol";
    public string musicParam  = "MusicVol";
    public string sfxParam    = "SFXVol";

    [Header("UI")]
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("Other Settings")]
    public Toggle fullscreenToggle;

    private bool _loaded;

    private void Start()
    {
        // Load from save, or defaults
        var data = SaveSystem.I ? SaveSystem.I.Data : new SaveData();

        if (masterSlider) masterSlider.value = Mathf.Clamp01(data.masterVolume);
        if (musicSlider)  musicSlider.value  = Mathf.Clamp01(data.musicVolume);
        if (sfxSlider)    sfxSlider.value    = Mathf.Clamp01(data.sfxVolume);

        if (fullscreenToggle) fullscreenToggle.isOn = Screen.fullScreen;

        ApplyAll();
        HookEvents();
        _loaded = true;
    }

    private void HookEvents()
    {
        if (masterSlider) masterSlider.onValueChanged.AddListener(_ => OnChanged());
        if (musicSlider)  musicSlider.onValueChanged.AddListener(_ => OnChanged());
        if (sfxSlider)    sfxSlider.onValueChanged.AddListener(_ => OnChanged());
        if (fullscreenToggle) fullscreenToggle.onValueChanged.AddListener(OnFullscreenChanged);
    }

    private void OnChanged()
    {
        ApplyAll();
        Persist();
    }

    private void OnFullscreenChanged(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void ApplyAll()
    {
        if (masterSlider) SetMixer(masterParam, masterSlider.value);
        if (musicSlider)  SetMixer(musicParam, musicSlider.value);
        if (sfxSlider)    SetMixer(sfxParam, sfxSlider.value);
    }

    private void Persist()
    {
        if (!SaveSystem.I || !_loaded) return;
        var d = SaveSystem.I.Data;
        d.masterVolume = masterSlider ? masterSlider.value : d.masterVolume;
        d.musicVolume  = musicSlider  ? musicSlider.value  : d.musicVolume;
        d.sfxVolume    = sfxSlider    ? sfxSlider.value    : d.sfxVolume;
        SaveSystem.I.Save();
    }

    private void SetMixer(string param, float linear01)
    {
        // Convert 0..1 to decibels: 0 -> -80 dB (mute), 1 -> 0 dB.
        float dB = (linear01 <= 0.0001f) ? -80f : Mathf.Log10(linear01) * 20f;
        if (mixer) mixer.SetFloat(param, dB);
    }
}