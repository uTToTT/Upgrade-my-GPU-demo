using System.Collections.Generic;
using UnityEngine;

public class KeySequenceUI
{
    private readonly Dictionary<KeyCode, SpriteRenderer> _keyRendererPair = new();

    private KeySpriteMap _keySpriteMap;

    public Dictionary<KeyCode, SpriteRenderer> KeyRendererPair => _keyRendererPair;

    public KeySequenceUI()
    {
        _keySpriteMap = GameManager.Instance.KeySpriteMap;
    }

    public void AddTip(KeyCode key, SpriteRenderer renderer)
    {
        Sprite sprite = _keySpriteMap.GetSprite(key);

        if (sprite == null)
            throw new System.ArgumentException(nameof(key));

        if (_keyRendererPair.ContainsKey(key))
            throw new System.ArgumentException(nameof(key));

        renderer.sprite = sprite;
        _keyRendererPair[key] = renderer;
    }

    public void SetTip(KeyCode keyCode)
    {
        if (!_keyRendererPair.ContainsKey(keyCode))
            throw new System.ArgumentException(nameof(keyCode));

        var renderer = _keyRendererPair[keyCode];
        renderer.enabled = true;
    }

    public void HideTip(KeyCode keyCode)
    {
        if (!_keyRendererPair.ContainsKey(keyCode))
            throw new System.ArgumentException(nameof(keyCode));

        _keyRendererPair[keyCode].enabled = false;
    }

    public void HideAllTips()
    {
        foreach (var item in _keyRendererPair.Values)
        {
            item.enabled = false;
        }
    }

    public void Clear() => _keyRendererPair.Clear();
}
