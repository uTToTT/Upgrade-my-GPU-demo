using NaughtyAttributes;
using UnityEngine;
using UTToTTGames.Debug;

public class VoodooController : MonoBehaviour, IDebuggable
{
    [SerializeField] private bool _debugging;
    [Required][SerializeField] private Voodoo _voodoo;

    private KeySequence _sequence;

    public bool Debugging => _debugging;

    #region ==== Init ====

    public void Init(KeySequenceUI ui)
    {
        _sequence = new KeySequence(this, ui, SequenceStartType.Key, true, _debugging);

        _sequence.SetEnterKey(KeyCode.Tab);

        _sequence.AddKey(KeyCode.W);
        _sequence.AddKey(KeyCode.A);
        _sequence.AddKey(KeyCode.D);
        _sequence.AddKey(KeyCode.W);
        _sequence.AddKey(KeyCode.S);
        _sequence.AddKey(KeyCode.A);
        _sequence.AddKey(KeyCode.S);
        _sequence.AddKey(KeyCode.D);

        SubOnEvents();
    }

    #endregion

    #region ==== Unity API ====

    private void OnDisable() => UnsubOnEvents();

    private void Update()
    {
        if (!PlayerInputManager.Instance.IsEnabled(InputActionType.Voodoo)) return;
        if (_sequence != null)
            _sequence.Update();
    }

    #endregion

    public void SetDebug(bool state) => _debugging = state;

    private void OnVoodooComplete()
    {
        DebugUtils.LogIfDebug(this, $"[{name}] Ritual completed!");
    }

    private void OnVoodooFail()
    {
        DebugUtils.LogWarningIfDebug(this, $"[{name}] Ritual failed!");
        _voodoo.CancelVoodoo();
    }

    private void OnKeySuccess()
    {
        DebugUtils.LogWarningIfDebug(this, $"[{name}] Key success!");
        _voodoo.CastVoodoo();
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

    private void SubOnEvents()
    {
        _sequence.SequenceSucñess += OnVoodooComplete;
        _sequence.Failed += OnVoodooFail;
        _sequence.KeySucñess += OnKeySuccess;
        _sequence.ModeChanged += OnModeChanged;
    }

    private void UnsubOnEvents()
    {
        _sequence.SequenceSucñess -= OnVoodooComplete;
        _sequence.Failed -= OnVoodooFail;
        _sequence.KeySucñess -= OnKeySuccess;
        _sequence.ModeChanged -= OnModeChanged;
    }
}
