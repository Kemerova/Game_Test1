using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Minimal HUD: HP bar, XP bar, timer. Hook via inspector.
/// </summary>
public class UIHud : MonoBehaviour
{
    public Health playerHealth;
    public Experience playerXP;
    public Slider hpBar;
    public Slider xpBar;
    public UnityEngine.UI.Text timerText; // optional timer text

    private float _time;

    private void Start()
    {
        if (playerHealth && hpBar)
        {
            hpBar.minValue = 0;
            hpBar.maxValue = playerHealth.maxHP;
            hpBar.value = playerHealth.currentHP;
        }
        if (playerXP && xpBar)
        {
            xpBar.minValue = 0;
            xpBar.maxValue = playerXP.xpToNext;
            xpBar.value = playerXP.currentXP;
        }
    }

    private void Update()
    {
        _time += Time.deltaTime;
        if (timerText)
        {
            int t = Mathf.FloorToInt(_time);
            int m = t / 60; int s = t % 60;
            timerText.text = $"{m:00}:{s:00}";
        }
        if (playerHealth && hpBar)
        {
            hpBar.value = playerHealth.currentHP;
        }
        if (playerXP && xpBar)
        {
            xpBar.maxValue = playerXP.xpToNext;
            xpBar.value = playerXP.currentXP;
        }
    }
}