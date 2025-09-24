using System.Collections.Generic;
using UnityEngine;
using UTToTTGames.Initialization;

public class CoolingManager : MonoBehaviour, IInitializable
{
    public static CoolingManager Instance { get; private set; }

    private readonly List<IHeatProducer> _heatProducers = new();
    private readonly List<ICoolingProvider> _coolingProviders = new();

    [Header("Environment Cooling")]
    [field: SerializeField] public float AmbientTemperature;
    
    public float TotalHeat { get; private set; }
    public float TotalCooling { get; private set; }
    public float ThermalFactor { get; private set; }

    #region ==== Init ====

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

    #endregion ===========

    #region ==== Unity API ====

    private void Update() => Recalculate();

    #endregion ================

    public void RegisterHeatProducer(IHeatProducer producer)
    {
        if (_heatProducers.Contains(producer))
            throw new System.ArgumentException(nameof(producer));

        _heatProducers.Add(producer);
    }

    public void UnregisterHeatProducer(IHeatProducer producer)
    {
        if (!_heatProducers.Contains(producer))
            throw new System.ArgumentException(nameof(producer));

        _heatProducers.Remove(producer);
    }

    public void RegisterCoolingProvider(ICoolingProvider provider)
    {
        if (_coolingProviders.Contains(provider))
            throw new System.ArgumentException(nameof(provider));

        _coolingProviders.Add(provider);
    }

    public void UnregisterCoolingProvider(ICoolingProvider provider)
    {
        if (!_coolingProviders.Contains(provider))
            throw new System.ArgumentException(nameof(provider));

        _coolingProviders.Remove(provider);
    }


    private void Recalculate()
    {
        TotalHeat = 0f;
        foreach (var h in _heatProducers)
            TotalHeat += h.GetHeatPerSecond();

        TotalCooling = 0f;
        foreach (var c in _coolingProviders)
            TotalCooling += c.GetCoolingPerSecond();

        ThermalFactor = TotalHeat > 0f
            ? Mathf.Clamp01(TotalCooling / TotalHeat)
            : 1f;

        foreach (var h in _heatProducers)
            h.SetCoolingFactor(ThermalFactor);
    }
}
