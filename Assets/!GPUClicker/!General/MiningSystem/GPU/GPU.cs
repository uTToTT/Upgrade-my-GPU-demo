using NaughtyAttributes;
using UnityEngine;

public class GPU : MonoBehaviour, IEnergyConsumer, IFactorProvider, IHeatProducer
{
    #region ==== Info ====

    [HorizontalLine]
    [BoxGroup("Info")]
    [ProgressBar("Work factor", 1, EColor.Blue)]
    public float _workFactorBar;
    [BoxGroup("Info")]
    [ProgressBar("Consumption", nameof(_maxConsumption), EColor.Yellow)]
    public float _currConsumption;
    [BoxGroup("Info")]
    [ProgressBar("Clock", nameof(_maxClock), EColor.Orange)]
    public float _currClock;
    [BoxGroup("Info")]
    [ProgressBar("Temperature", nameof(_maxTemperature), EColor.Red)]
    public float _currTemperature;

    #endregion ===========

    private GPUConfig _config;
    private FactorController _factorController;
    private FireController _fireController;

    private float _maxConsumption;
    private float _maxClock;
    private float _maxTemperature = 100f;

    private float _throttleStart;

    private float _coolingFactor;

    #region ==== Properties ====

    public FactorController FactorController => _factorController;

    public float WorkFactor => _factorController.GetValue();
    public bool IsMining => WorkFactor > 0f;
    public bool IsOnFire => _fireController.IsOnFire;

    #endregion =================

    #region ==== Init ====

    public bool Init()
    {
        _factorController = new FactorController();
        _currTemperature = CoolingManager.Instance.AmbientTemperature;

        EnergyManager.Instance.RegisterConsumer(this);
        CoolingManager.Instance.RegisterHeatProducer(this);

        return true;
    }

    public void InitConfig(GPUConfig config)
    {
        _config = Instantiate(config);

        _maxConsumption = _config.Consumption;
        _maxClock = _config.Clock;
        _maxTemperature = _config.MaxHeat;

        _throttleStart = _maxTemperature * _config.ThrottleThreshold;
        _fireController = new(_maxTemperature, _config.FireDelay);

        EnergyManager.Instance.UnregisterConsumer(this);
        EnergyManager.Instance.RegisterConsumer(this);

        CoolingManager.Instance.UnregisterHeatProducer(this);
        CoolingManager.Instance.RegisterHeatProducer(this);
    }

    #endregion ===========

    #region ==== Unity API ====

    private void Update()
    {
        if (_config == null) return;

        UpdateTemperature(_coolingFactor);
        UpdateThrottleFactor();

        _fireController.Update(_currTemperature, Time.deltaTime);

        if (IsOnFire)
            _factorController.AddModifier(FactorController.FACTOR_FIRE_ID, 0); 
        else
            _factorController.RemoveModifier(FactorController.FACTOR_FIRE_ID);
    }


    private void LateUpdate()
    {
#if UNITY_EDITOR
        _workFactorBar = WorkFactor;
#endif
    }

    #endregion ================

    #region ==== Energy ====

    public float GetRatedConsumptionPerSecond()
    {
        if (_config == null) return 0f;

        return _config.Consumption;
    }

    public float GetConsumptionPerSecond()
    {
        if (_config == null) return 0f;

        _currConsumption = _config.Consumption * WorkFactor;
        return _currConsumption;
    }

    #endregion =============

    #region ==== Heat ====

    public void Extinguish() => _fireController.ExtinguishFire();

    public float GetHeatPerSecond()
    {
        if (_config == null) return 0f;

        return _config.HeatPerSecond;
    }

    private void UpdateTemperature(float thermalFactor)
    {
        float heatGenerated = _config.HeatPerSecond * WorkFactor;

        float ambientTemperature = CoolingManager.Instance.AmbientTemperature;
        float coolingPower = heatGenerated * thermalFactor;

        float equilibriumTemp = ambientTemperature + (heatGenerated - coolingPower);

        equilibriumTemp = Mathf.Clamp(equilibriumTemp, ambientTemperature, _maxTemperature);

        float coolingRate = _config.CoolingResponseSpeed;
        _currTemperature = Mathf.Lerp(
            _currTemperature,
            equilibriumTemp,
            1f - Mathf.Exp(-coolingRate * Time.deltaTime)
        );
    }


    private void UpdateThrottleFactor()
    {
        if (_currTemperature <= _throttleStart)
        {
            _factorController.AddModifier(FactorController.FACTOR_THROTTLE_ID, 1f);
            return;
        }

        float progress = Mathf.InverseLerp(_throttleStart, _maxTemperature, _currTemperature);
        float factor = Mathf.Lerp(1f, _config.MinFactorAtMaxTemp, progress);

        _factorController.AddModifier(FactorController.FACTOR_THROTTLE_ID, factor);
    }

    #endregion ===========

    public float GetMiningIncomePerSecond()
    {
        if (_config == null) return 0f;

        _currClock = _config.Clock * WorkFactor;
        return _currClock;
    }

    #region ==== Factor ====

    public void SetEnergyFactor(float factor) =>
        _factorController.AddModifier(FactorController.FACTOR_ENERGY_ID, factor);

    public void SetVoodooFactor(float factor, float duration) =>
        _factorController.AddTemporaryModifier(FactorController.FACTOR_VOODOO_ID, factor, duration);

    public void SetCoolingFactor(float coolingFactor) =>
        _coolingFactor = coolingFactor;

    #endregion =============
}
