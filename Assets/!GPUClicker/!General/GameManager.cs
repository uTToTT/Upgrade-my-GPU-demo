using NaughtyAttributes;
using UnityEngine;
using UTToTTGames.Initialization;

public class GameManager : MonoBehaviour, IInitializable
{
    [SerializeField] private PlayerController _playerController;
    [SerializeField] private KeySpriteMap _keySpriteMap;

    private IPersistentData _persistentPlayerData;
    private IDataProvider _dataProvider;

    public IPersistentData PersistentPlayerData => _persistentPlayerData;
    public KeySpriteMap KeySpriteMap => _keySpriteMap;

    public Transform PlayerTransform { get; private set; }
    public Wallet Wallet { get; private set; }

    public static GameManager Instance { get; private set; }

    #region ==== Init ====

    public bool Init()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return false;
        }
        Instance = this;

        InitData();
        InitWallet();
        InitFields();

        return true;
    }

    private void InitData()
    {
        _persistentPlayerData = new PersistentData();
        _dataProvider = new DataLocalProvider(_persistentPlayerData);

        LoadDataOrInit();
    }

    private void LoadDataOrInit()
    {
        if (_dataProvider.TryLoad() == false)
            _persistentPlayerData.PlayerData = new PlayerData();
    }

    private void InitWallet()
    {
        Wallet = new Wallet(_persistentPlayerData);
    }

    private void InitFields()
    {
        PlayerTransform = _playerController.transform;
    }

    #endregion ===========


    [Button]
    public void SaveGame() => _dataProvider.Save();
    [Button]
    private void DeleteSaves() => _dataProvider.Delete();
}
