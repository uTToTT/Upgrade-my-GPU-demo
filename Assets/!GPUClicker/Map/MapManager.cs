using System.Collections.Generic;
using UnityEngine;
using UTToTTGames.Initialization;

public class MapManager : MonoBehaviour, IInitializable
{
    [SerializeField] private List<Location> _locations;
    
    private Dictionary<Locations, Location> _locationsDict = new();

    private TeleportController _teleportController;

    #region ==== Init ====

    public bool Init()
    {
        _teleportController = new();

        foreach (var location in _locations)
        {
            if (_locationsDict.ContainsKey(location.LocationType))
                throw new System.ArgumentException(nameof(location));

            _locationsDict[location.LocationType] = location;
        }

        EnterLocation(GetLastLocationType());

        return true;
    }

    #endregion ===========

    public void EnterLocation(Locations locationType)
    {
        foreach (var loc in _locations)
        {
            loc.SetActive(false);
        }

        var targetLoc = GetLocation(locationType);
        targetLoc.SetActive(true);
        TeleportPlayer(targetLoc);
        GameManager.Instance.PersistentPlayerData.PlayerData.LastLocation = locationType;
        GameManager.Instance.SaveGame();
    }

    private Locations GetLastLocationType() => 
        GameManager.Instance.PersistentPlayerData.PlayerData.LastLocation;

    private Location GetLocation(Locations location)
    {
        if (!_locationsDict.ContainsKey(location))
            throw new System.ArgumentException(nameof(location));

            return _locationsDict[location];
    }

    private void TeleportPlayer(ITeleportDestination location)
    {
        _teleportController.Teleport(location, GameManager.Instance.PlayerTransform);
    }
}
