using UnityEngine;

public class Location : MonoBehaviour, ITeleportDestination
{
    [SerializeField] private GameObject _locationParent;
    [SerializeField] private Locations _locationType;
    [SerializeField] private Transform _entryPoint;

    #region ==== Properties ====
    
    public Transform EntryPoint => _entryPoint;
    public Locations LocationType => _locationType;

    #endregion =================

    public void SetActive(bool state) => _locationParent.SetActive(state);
}
