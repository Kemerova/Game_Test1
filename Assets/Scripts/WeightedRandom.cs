using System;
using System.Collections.Generic;
using UnityEngine;

public static class WeightedRandom
{
    private static System.Random _rng = new System.Random();

    public static T Pick<T>(IList<T> items, Func<T, float> weightSelector)
    {
        float total = 0f;
        foreach (var it in items) total += Mathf.Max(0f, weightSelector(it));
        if (total <= 0f) return default;

        float r = (float)(_rng.NextDouble()) * total;
        float accum = 0f;
        foreach (var it in items)
        {
            accum += Mathf.Max(0f, weightSelector(it));
            if (r <= accum) return it;
        }
        return items[items.Count - 1];
    }
}