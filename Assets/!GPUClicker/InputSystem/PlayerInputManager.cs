using System;
using System.Collections.Generic;
using UnityEngine;
using UTToTTGames.Initialization;

public enum InputActionType
{
    Movement,
    Voodoo,
    FireExtinguisher,

    HideShop,
    HideStorage,
    Interact,
}

public class PlayerInputManager : MonoBehaviour, IInitializable
{
    public static PlayerInputManager Instance { get; private set; }

    private HashSet<InputActionType> _disabledActions = new();

    private Dictionary<KeyCode, InputActionType> _keysConsumedThisFrame = new();

    private readonly Dictionary<InputActionType, KeyCode[]> _defaultBinds = new()
    {
        { InputActionType.HideShop, new[] { KeyCode.Escape, KeyCode.E } },
        { InputActionType.HideStorage, new[] { KeyCode.Escape, KeyCode.E } },
        { InputActionType.Interact, new[] { KeyCode.E } },
        { InputActionType.Voodoo, new[] { KeyCode.Tab } }
    };

    private readonly InputActionType[] _priorityOrder = new[]
    {
        InputActionType.HideShop,
        InputActionType.HideStorage,
        InputActionType.Interact,
        InputActionType.Voodoo,
        InputActionType.FireExtinguisher,
        InputActionType.Movement
    };

    public bool Init()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return false;
        }

        Instance = this;
        return true;
    }

    private void Update()
    {
        _keysConsumedThisFrame.Clear();

        foreach (var action in _priorityOrder)
        {
            if (IsActionTriggeredInternal(action))
            {
            }
        }
    }

    private bool IsActionTriggeredInternal(InputActionType action)
    {
        if (!_defaultBinds.TryGetValue(action, out var keys))
            return false;

        if (!IsEnabled(action))
            return false;

        foreach (var key in keys)
        {
            if (_keysConsumedThisFrame.TryGetValue(key, out var owner))
            {
                if (owner != action)
                {
                    Debug.Log($"Continue [{action}]");
                    continue;

                }
            }

            if (Input.GetKeyDown(key))
            {
                _keysConsumedThisFrame[key] = action;
                Debug.Log($"Process [{action}]");
                return true;
            }
        }

        return false;
    }

    public bool IsActionTriggered(InputActionType action)
    {
        return IsActionTriggeredInternal(action);
    }

    public bool IsEnabled(InputActionType action)
    {
        return !_disabledActions.Contains(action);
    }

    public void SetEnabled(InputActionType action, bool state)
    {
        if (state)
            _disabledActions.Remove(action);
        else
            _disabledActions.Add(action);
    }

    public void DisableAll()
    {
        foreach (InputActionType action in Enum.GetValues(typeof(InputActionType)))
            _disabledActions.Add(action);
    }

    public void EnableAll()
    {
        _disabledActions.Clear();
    }
}
