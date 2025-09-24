using System.Collections.Generic;
using UnityEngine;
using UTToTTGames.Initialization;

public class EnergyManager : MonoBehaviour, IInitializable
{
    public static EnergyManager Instance { get; private set; }

    private readonly List<IEnergyProvider> _providers = new();
    private readonly List<IEnergyConsumer> _consumers = new();

    public float CurrentProduction { get; private set; }
    public float RatedConsumption { get; private set; }
    public float CurrentConsumption { get; private set; }
    public float NetEnergy => CurrentProduction - RatedConsumption;

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

    public void RegisterProducer(IEnergyProvider producer)
    {
        if (!_providers.Contains(producer))
            _providers.Add(producer);
    }

    public void UnregisterProducer(IEnergyProvider producer) => _providers.Remove(producer);

    public void RegisterConsumer(IEnergyConsumer consumer)
    {
        if (_consumers.Contains(consumer))
            throw new System.ArgumentException(nameof(consumer));

        _consumers.Add(consumer);
    }

    public void UnregisterConsumer(IEnergyConsumer consumer)
    {
        if (!_consumers.Contains(consumer))
            throw new System.ArgumentException(nameof(consumer));

        _consumers.Remove(consumer);
    }

    private void Update()
    {
        Recalculate();
    }

    private void Recalculate()
    {
        CurrentProduction = 0f;
        foreach (var p in _providers)
            CurrentProduction += p.GetProductionPerSecond();

        float totalNeed = 0f;
        foreach (var c in _consumers)
            totalNeed += c.GetRatedConsumptionPerSecond();

        float factor =
            totalNeed > 0f ?
            Mathf.Clamp01(CurrentProduction / totalNeed) : 1f;

        foreach (var c in _consumers)
            c.SetEnergyFactor(factor);

        RatedConsumption = totalNeed;

        CurrentConsumption = 0f;
        foreach (var c in _consumers)
            CurrentConsumption += c.GetConsumptionPerSecond();
    }

}
