using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class LocationButton : MonoBehaviour
{
    public event Action<LocationButton, Locations> Click;

    [SerializeField] private Button _button;
    [SerializeField] private Locations _location;

    public Locations Location => _location;

    #region ==== Unity API ====

    private void OnValidate()
    {
#if UNITY_EDITOR
        if (_button == null)
            _button = GetComponent<Button>();
#endif
    }

    private void OnEnable() => _button.onClick.AddListener(OnClick);
    private void OnDisable() => _button.onClick.RemoveListener(OnClick);

    #endregion ================

    public void Select() => _button.interactable = false;
    public void Unselect() => _button.interactable = true;

    private void OnClick() => Click?.Invoke(this,_location);
}
