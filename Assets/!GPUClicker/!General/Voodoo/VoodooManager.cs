using NaughtyAttributes;
using UnityEngine;
using UTToTTGames.Initialization;

public class VoodooManager : MonoBehaviour, IInitializable
{
    [SerializeField] private VoodooFactory _factory;
    [SerializeField] private VoodooUI _ui;
    [SerializeField] private VoodooController _controller;
    [SerializeField] private Voodoo _voodoo;

    private KeySequenceUI _keySequenceUI;

    #region ==== Init ====

    public bool Init()
    {
        LoadData();

        _keySequenceUI = _ui.Init();
        _controller.Init(_keySequenceUI);

        return true;
    }

    [Button]
    public void LoadData()
    {
        var selectedVoodoo = GameManager.Instance.PersistentPlayerData.PlayerData.SelectedVoodoo;
        var voodooConfig = _factory.Get(selectedVoodoo);

        _voodoo.InitConfig(voodooConfig);
    }

    #endregion ===========
}
