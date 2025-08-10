using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Tracks XP & levels. Hook OnLevelUp to show a draft UI later.
/// </summary>
public class Experience : MonoBehaviour
{
    public int currentLevel = 1;
    public int currentXP = 0;
    public int xpToNext = 20;
    public AnimationCurve xpCurve = AnimationCurve.Linear(1, 20, 20, 160);

    [System.Serializable] public class LevelUpEvent : UnityEvent<int> {}
    public LevelUpEvent OnLevelUp;

    public void AddXP(int amount)
    {
        currentXP += Mathf.Max(0, amount);
        while (currentXP >= xpToNext)
        {
            currentXP -= xpToNext;
            currentLevel++;
            xpToNext = Mathf.CeilToInt(xpCurve.Evaluate(currentLevel));
            OnLevelUp?.Invoke(currentLevel);
        }
    }
}