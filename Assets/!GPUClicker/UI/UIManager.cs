using System.Collections.Generic;
using UnityEngine;
using UTToTTGames.Initialization;

public class UIManager : MonoBehaviour, IInitializable
{
    public static UIManager Instance { get; private set; }

    private Stack<IClosableUI> _uiStack = new();
    private Dictionary<InputActionType, int> _disabledStack = new();

    public bool Init()
    {
        Instance = this;
        return true;
    }

    private void Update()
    {
        if (_uiStack.Count == 0) return;

        var top = _uiStack.Peek();

        if (PlayerInputManager.Instance.IsActionTriggered(top.HideAction))
        {
            Hide(top);
        }
    }

    public void Show(IClosableUI ui)
    {
        _uiStack.Push(ui);
        ui.Show();

        foreach (var action in ui.DisabledActionsOnShow)
            DisableAction(action);

        foreach (var action in ui.DisabledActionsOnHide)
            EnableAction(action);
    }

    public void Hide(IClosableUI ui)
    {
        if (_uiStack.Count > 0 && _uiStack.Peek() == ui)
        {
            _uiStack.Pop();
            ui.Hide();

            foreach (var action in ui.DisabledActionsOnShow)
                EnableAction(action);

            foreach (var action in ui.DisabledActionsOnHide)
                DisableAction(action);
        }
    }


    public void DisableAction(InputActionType action)
    {
        if (_disabledStack.ContainsKey(action))
            _disabledStack[action]++;
        else
            _disabledStack[action] = 1;

        PlayerInputManager.Instance.SetEnabled(action, false);
    }

    public void EnableAction(InputActionType action)
    {
        if (!_disabledStack.ContainsKey(action)) return;

        _disabledStack[action]--;
        if (_disabledStack[action] <= 0)
        {
            _disabledStack.Remove(action);
            PlayerInputManager.Instance.SetEnabled(action, true);
        }
    }
}
