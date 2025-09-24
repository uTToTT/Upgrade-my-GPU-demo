using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MapUI : MonoBehaviour
{
    [SerializeField] private MapManager _mapManager;
    [SerializeField] private GameObject _map;
    [SerializeField] private TriggerZone[] _showMapZones;
    [Space]
    [SerializeField] private List<LocationButton> _buttons;

    #region ==== Unity API ====

    private void OnEnable() => SubOnClick();
    private void OnDisable() => UnsubOnClick();

    #endregion ================

    private void SubOnClick()
    {
        foreach (var button in _buttons)
        {
            button.Click += OnClick;
        }

        foreach (var zone in _showMapZones)
        {
            zone.Interact += ShowMap;
        }
    }

    private void UnsubOnClick()
    {
        foreach (var button in _buttons)
        {
            button.Click -= OnClick;
        }

        foreach (var zone in _showMapZones)
        {
            zone.Interact -= ShowMap;
        }
    }

    private void OnClick(LocationButton button, Locations location)
    {
        _mapManager.EnterLocation(location);
        HideMap();
    }

    private LocationButton GetButton(Locations location)
    {
       var button = _buttons.FirstOrDefault(b => b.Location == location);

        if (button == null)
            throw new System.ArgumentOutOfRangeException(nameof(button));

        return button;
    }

    private void UnselectButtons()
    {
        foreach (var button in _buttons)
        {
            button.Unselect();
        }
    }

    public void ShowMap()
    {
        var loc = GameManager.Instance.PersistentPlayerData.PlayerData.LastLocation;
        var button = GetButton(loc);

        UnselectButtons();
        button.Select();
        _map.SetActive(true);
        PlayerInputManager.Instance.DisableAll();
    }

    public void HideMap()
    {
        _map.SetActive(false);
        PlayerInputManager.Instance.EnableAll();
    }
}
