using NaughtyAttributes;
using UnityEngine;
using UTToTTGames.Initialization;

public class ShopBootstrap : MonoBehaviour, IInitializable
{
    [Required]
    [SerializeField] private Shop _shop;

    public bool Init()
    {
        InitShop();

        return true;
    }

    private void InitShop()
    {
        var persistentPlayerData = GameManager.Instance.PersistentPlayerData;
        var wallet = GameManager.Instance.Wallet;

        OpenShopItemChecker openShopItemChecker = new(persistentPlayerData);
        SelectedShopItemChecker selectedShopItemChecker = new(persistentPlayerData);
        ShopItemSelector shopItemSelector = new(persistentPlayerData);
        ShopItemUnlocker shopItemUnlocker = new(persistentPlayerData);

        _shop.Init(wallet, openShopItemChecker,
            selectedShopItemChecker, shopItemSelector, shopItemUnlocker);
    }
}
