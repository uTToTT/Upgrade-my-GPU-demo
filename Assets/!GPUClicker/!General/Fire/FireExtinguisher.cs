using UnityEngine;
using UTToTTGames.Debug;

public class FireExtinguisher : MonoBehaviour, IDebuggable
{
    [SerializeField] private bool _debugging;
    [SerializeField] private TriggerZone _triggerZone;

    private KeySequence _sequence;
    private GPU _gpu;

    public bool Debugging => _debugging;

    #region ==== Init ====

    public void Init(KeySequenceUI ui, GPU GPU)
    {
        _gpu = GPU;
        _sequence = new KeySequence(this, ui, SequenceStartType.Code, false, _debugging);

        _sequence.AddKey(KeyCode.Mouse0);
        _sequence.AddKey(KeyCode.Mouse1);
        _sequence.AddKey(KeyCode.Mouse0);
        _sequence.AddKey(KeyCode.Mouse1);
        _sequence.AddKey(KeyCode.Mouse0);

        _sequence.AddKey(KeyCode.Mouse1);
        _sequence.AddKey(KeyCode.Mouse1);
        _sequence.AddKey(KeyCode.Mouse1);
        _sequence.AddKey(KeyCode.Mouse0);
        _sequence.AddKey(KeyCode.Mouse0);

        _sequence.AddKey(KeyCode.Mouse1);
        _sequence.AddKey(KeyCode.Mouse0);
        _sequence.AddKey(KeyCode.Mouse1);
        _sequence.AddKey(KeyCode.Mouse1);
        _sequence.AddKey(KeyCode.Mouse0);

        _sequence.AddKey(KeyCode.Mouse1);
        _sequence.AddKey(KeyCode.Mouse0);
        _sequence.AddKey(KeyCode.Mouse0);
        _sequence.AddKey(KeyCode.Mouse0);
        _sequence.AddKey(KeyCode.Mouse1);

        SubOnEvents();
    }

    #endregion ===========

    #region ==== Unity API ====

    private void OnDisable() => UnsubOnEvents();

    private void Update()
    {
        if (!PlayerInputManager.Instance.IsEnabled(InputActionType.FireExtinguisher)) return;
        if (_sequence != null)
            _sequence.Update();
    }

    #endregion ================

    public void SetDebug(bool state) => _debugging = state;

    private void OnSequenceComplete()
    {
        DebugUtils.LogIfDebug(this, $"[{name}] Ritual completed!");
        TryExtinguish();
    }

    private void OnSequenceFail()
    {
        DebugUtils.LogWarningIfDebug(this, $"[{name}] Ritual failed!");
    }

    private void OnKeySuccess()
    {
        DebugUtils.LogWarningIfDebug(this, $"[{name}] Key success!");
    }

    private void OnModeChanged(bool isSequence)
    {
        if (isSequence)
        {
            PlayerInputManager.Instance.SetEnabled(InputActionType.Movement, false);
        }
        else
        {
            PlayerInputManager.Instance.SetEnabled(InputActionType.Movement, true);
        }
    }


    private void SwitchSequenceMode() => _sequence.ToggleSequenceMode();

    private void SubOnEvents()
    {
        _sequence.SequenceSucñess += OnSequenceComplete;
        _sequence.Failed += OnSequenceFail;
        _sequence.KeySucñess += OnKeySuccess;
        _sequence.ModeChanged += OnModeChanged;

        _triggerZone.Interact += SwitchSequenceMode;
    }

    private void UnsubOnEvents()
    {
        _sequence.SequenceSucñess -= OnSequenceComplete;
        _sequence.Failed -= OnSequenceFail;
        _sequence.KeySucñess -= OnKeySuccess;
        _sequence.ModeChanged -= OnModeChanged;

        _triggerZone.Interact -= SwitchSequenceMode;
    }

    private void TryExtinguish()
    {
        if (_gpu.IsOnFire)
        {
            _gpu.Extinguish();
            DebugUtils.LogIfDebug(this, $"[{name}] Player extinguished the fire!");
        }
    }
}
