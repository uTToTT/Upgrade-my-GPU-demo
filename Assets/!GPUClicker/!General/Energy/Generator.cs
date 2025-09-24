using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UTToTTGames.Initialization;

public class Generator : MonoBehaviour, IEnergyProvider, IInitializable
{
    private GeneratorConfig _config;

    public float GetProductionPerSecond()
    {
        if (_config == null) return 0;

        return _config.Production;
    }

    public bool Init()
    {
        EnergyManager.Instance.RegisterProducer(this);
        return true;
    }

    public void SetConfig(GeneratorConfig config)
    {
        _config = Instantiate(config);
    }


}
