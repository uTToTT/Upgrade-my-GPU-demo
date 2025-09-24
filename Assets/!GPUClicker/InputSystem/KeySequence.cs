using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UTToTTGames.Debug;
using UTToTTGames.Routines;

public enum SequenceStartType
{
    Key,
    Code,
}

public class KeySequence : IDebuggable
{
    public event Action Failed;
    public event Action SequenceSucñess;
    public event Action KeySucñess;
    public event Action<bool> ModeChanged;

    private bool _debugging;
    private bool _isInSequenceMode;
    private bool _isDelay;
    private bool _resetOnFailed;

    private int _currStep = 0;
    private int _lastStep = -1;
    private float _delay = 1f;

    private KeyCode _keySequenceActivate;
    private SequenceStartType _sequenceStartType;

    private MonoBehaviour _target;
    private KeySequenceUI _ui;
    private Coroutine _routine;

    private List<KeyCode> _targetSequence = new();

    public bool Debugging => _debugging;

    public KeySequence(
        MonoBehaviour mono,
        KeySequenceUI ui,
        SequenceStartType sequenceStartType = SequenceStartType.Key,
        bool resetOnFailed = true,
        bool debugging = false)
    {
        _target = mono;
        _ui = ui;
        _sequenceStartType = sequenceStartType;
        _resetOnFailed = resetOnFailed;
        _debugging = debugging;
    }

    public void Update()
    {
        if (_sequenceStartType == SequenceStartType.Key &&
            Input.GetKeyDown(_keySequenceActivate))
        {
            ToggleSequenceMode();
        }

        if (_isInSequenceMode)
        {
            ListenForSequence();
        }
    }

    public void SetDelay(float delay)
    {
        if (delay < 0)
            throw new ArgumentOutOfRangeException(nameof(delay));
    }

    public void SetEnterKey(KeyCode key) => _keySequenceActivate = key;

    public void AddKey(KeyCode key)
    {
        _targetSequence.Add(key);
        _ui.HideTip(key);
    }

    public void SetDebug(bool state) => _debugging = state;

    public void ToggleSequenceMode()
    {
        _isInSequenceMode = !_isInSequenceMode;
        ResetProgress();

        if (_isInSequenceMode)
        {
            PlayerInputManager.Instance.SetEnabled(InputActionType.Movement, false);
        }
        else
        {
            PlayerInputManager.Instance.SetEnabled(InputActionType.Movement, true);
            StopNextKey();
            _isDelay = false;
        }
    }

    private void ResetProgress()
    {
        _currStep = 0;
        _lastStep = -1;
        DebugUtils.LogIfDebug(this,
            $"[{_target.name}] Sequence reset.");

        _ui.HideAllTips();
    }

    private void ListenForSequence()
    {
        if (_isDelay) return;

        if (_currStep >= _targetSequence.Count)
            ResetProgress();

        var waited = _targetSequence[_currStep];

        if (_currStep != _lastStep)
        {
            _lastStep = _currStep;
            _ui.SetTip(waited);

            DebugUtils.LogIfDebug(this,
                $"[{_target.name}] Waiting [{waited}] [{_currStep + 1}/{_targetSequence.Count}]");
        }

        if (Input.GetKeyDown(waited))
        {
            KeySucñess?.Invoke();
            _ui.HideTip(waited);
            StartNextKey();
        }
        else
        {
            foreach (var key in _targetSequence)
            {
                if (Input.GetKeyDown(key))
                {
                    Failed?.Invoke();

                    if (_resetOnFailed)
                        ResetProgress();

                    DebugUtils.LogWarningIfDebug(this,
                        $"[{_target.name}] Sequence failed!");

                    return;
                }
            }
        }
    }

    private void StartNextKey() =>
        _target.StartControlledRoutine(ref _routine, NextKey(), $"[{_target.name}] Next key.");

    private void StopNextKey() =>
        _target.StopControlledRoutine(ref _routine, $"[{_target.name}] Next key.");

    private IEnumerator NextKey()
    {
        _isDelay = true;
        yield return DelayedExecutor.Instance.Wait(_delay, TimeType.Scaled);

        _currStep++;

        if (_currStep >= _targetSequence.Count)
        {
            DebugUtils.LogIfDebug(this,
                $"[{_target.name}] Sequence completed!");

            SequenceSucñess?.Invoke();
            ResetProgress();
        }

        _isDelay = false;
        _routine = null;
    }
}
