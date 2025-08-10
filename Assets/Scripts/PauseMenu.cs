using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Simple pause menu that can be toggled with ESC key.
/// Handles time scaling and provides basic menu options.
/// </summary>
public class PauseMenu : MonoBehaviour
{
    [Header("UI")]
    public GameObject pausePanel;
    public GameObject settingsPanel;

    [Header("Input")]
    public KeyCode pauseKey = KeyCode.Escape;

    private bool _isPaused;
    private bool _wasTimeScaleZero;

    private void Update()
    {
        if (Input.GetKeyDown(pauseKey))
        {
            TogglePause();
        }
    }

    public void TogglePause()
    {
        if (_isPaused)
            Resume();
        else
            Pause();
    }

    public void Pause()
    {
        if (_isPaused) return;
        
        _wasTimeScaleZero = Time.timeScale <= 0f;
        _isPaused = true;
        Time.timeScale = 0f;
        
        if (pausePanel) pausePanel.SetActive(true);
    }

    public void Resume()
    {
        if (!_isPaused) return;
        
        _isPaused = false;
        if (!_wasTimeScaleZero) Time.timeScale = 1f;
        
        if (pausePanel) pausePanel.SetActive(false);
        if (settingsPanel) settingsPanel.SetActive(false);
    }

    public void OpenSettings()
    {
        if (settingsPanel) settingsPanel.SetActive(true);
        if (pausePanel) pausePanel.SetActive(false);
    }

    public void CloseSettings()
    {
        if (settingsPanel) settingsPanel.SetActive(false);
        if (pausePanel) pausePanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        StatsTracker.I?.ResetRun();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitToDesktop()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }
}