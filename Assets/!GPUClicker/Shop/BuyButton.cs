using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    public event Action Click;

    [SerializeField] private Button _button;
    [SerializeField] private TMP_Text _text;

    [SerializeField] private Color _lockColor;
    [SerializeField] private Color _unlockColor;

    [SerializeField, Range(0f, 1f)] private float _lockAnimationDuration = 0.4f;
    [SerializeField, Range(0.5f, 5f)] private float _lockAnimationStrength = 2f;

    private bool _isLock;

    private void OnEnable() => _button.onClick.AddListener(OnButtonClick);
    private void OnDisable() => _button.onClick.RemoveListener(OnButtonClick);

    public void UpdateText(float price) =>
        _text.text = NumberFormatter.Format(NumberFormat.Compact, price);

    public void Lock()
    {
        _isLock = true;
        _text.color = _lockColor;

    }

    public void Unlock()
    {
        _isLock = false;
        _text.color = _unlockColor;
    }

    private void OnButtonClick()
    {
        if (_isLock)
        {
            transform.DOShakePosition(_lockAnimationDuration, _lockAnimationStrength);
            return;
        }

        Click?.Invoke();
    }
}
