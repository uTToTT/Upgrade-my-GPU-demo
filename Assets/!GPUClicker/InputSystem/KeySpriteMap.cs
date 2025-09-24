using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "KeySpriteMap", menuName = "Map/KeySprite")]
public class KeySpriteMap : ScriptableObject
{
    [Serializable]
    private class KeySprite
    {
        public KeyCode Key;
        public Sprite Sprite;
    }

    [SerializeField] private List<KeySprite> _keySprites = new();

    private Dictionary<KeyCode, Sprite> _spritesByKey;

    public Sprite GetSprite(KeyCode key)
    {
        if (_spritesByKey == null || _spritesByKey.Count == 0)
            BuildDictionary();

        return _spritesByKey.TryGetValue(key, out var sprite) ? sprite : null;
    }

    #region ==== Unity API ====

    private void OnEnable() => BuildDictionary();

    #endregion ================

    private void BuildDictionary()
    {
        _spritesByKey = new Dictionary<KeyCode, Sprite>();

        foreach (var entry in _keySprites)
        {
            if (entry == null || entry.Sprite == null)
                continue;

            if (!_spritesByKey.ContainsKey(entry.Key))
            {
                _spritesByKey.Add(entry.Key, entry.Sprite);
            }
            else
            {
                Debug.LogWarning($"Key {entry.Key} already exists in {name}, ignoring duplicate.");
            }
        }
    }
}
