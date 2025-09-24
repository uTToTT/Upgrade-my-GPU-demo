using NaughtyAttributes;
using UnityEngine;
using UTToTTGames.Debug;
using UTToTTGames.Initialization;

public class MiningManager : MonoBehaviour, IInitializable, IDebuggable
{
    public static MiningManager Instance { get; private set; }

    [SerializeField] private bool _debugging;
    [Required]
    [SerializeField] private GPU _gpu;
    [Required]
    [SerializeField] private GPUFactory _factory;

    public float TotalIncomePerSecond { get; private set; }

    public bool Debugging =>_debugging;

    public bool Init()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return false;
        }
        Instance = this;

        InitGPU();
        LoadGPUData();

        return true;
    }

    private void InitGPU()
    {
        _gpu.Init();
    }

    [Button]
    public void LoadGPUData()
    {
        var selectedGPU = GameManager.Instance.PersistentPlayerData.PlayerData.SelectedGPU;
        var GPUConfig = _factory.Get(selectedGPU);

        _gpu.InitConfig(GPUConfig);
        DebugUtils.LogIfDebug(this,
            $"[{selectedGPU}] Selected.");
    }

    private void Update()
    {
        RecalculateIncome();
        AddIncome();
    }

    private void RecalculateIncome()
    {
        TotalIncomePerSecond = _gpu.GetMiningIncomePerSecond();
    }

    private void AddIncome()
    {
        var income = TotalIncomePerSecond * Time.deltaTime;
        GameManager.Instance.Wallet.AddMoney(income);
    }

    public void SetDebug(bool state) => _debugging = state;
}
