using System.Collections.Generic;
using UnityEngine;

public class FactorController
{
    public const string FACTOR_ENERGY_ID = "energy";
    public const string FACTOR_THROTTLE_ID = "throttle";
    public const string FACTOR_FIRE_ID = "fire";
    public const string FACTOR_VOODOO_ID = "voodoo";

    private class Modifier
    {
        public float Multiplier;
        public float? ExpireTime;
    }

    private Dictionary<string, Modifier> _modifiers = new();
    private float _baseValue = 1f;

    public FactorController(float baseValue = 1f) => _baseValue = baseValue;

    public void AddModifier(string id, float multiplier)
    {
        if (_modifiers.ContainsKey(id))
        {
            _modifiers[id].Multiplier = multiplier;
            _modifiers[id].ExpireTime = null;
        }
        else
        {
            _modifiers[id] = new Modifier { Multiplier = multiplier, ExpireTime = null };
        }
    }

    public void AddTemporaryModifier(string id, float multiplier, float durationSeconds)
    {
        float expireTime = Time.time + durationSeconds;

        if (_modifiers.ContainsKey(id))
        {
            _modifiers[id].Multiplier = multiplier;
            _modifiers[id].ExpireTime = expireTime;
        }
        else
        {
            _modifiers[id] = new Modifier { Multiplier = multiplier, ExpireTime = expireTime };
        }
    }

    public void RemoveModifier(string id) => _modifiers.Remove(id);

    public float GetValue()
    {
        float result = _baseValue;
        List<string> expired = null;

        foreach (var kvp in _modifiers)
        {
            var modifier = kvp.Value;
            if (modifier.ExpireTime != null && modifier.ExpireTime <= Time.time)
            {
                expired ??= new List<string>();
                expired.Add(kvp.Key);
                continue;
            }

            result *= modifier.Multiplier;
        }

        if (expired != null)
        {
            foreach (var key in expired)
                _modifiers.Remove(key);
        }

        return result;
    }

    public void Clear() => _modifiers.Clear();
}
