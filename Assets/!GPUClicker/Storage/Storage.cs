using System.Linq;
using TMPro;
using UnityEngine;
using UTToTTGames.Initialization;

public class Storage : MonoBehaviour, IInitializable, IClosableUI
{
    [SerializeField] private GameObject _storage;
    [SerializeField] private ShopPanel _shopPanel;
    [SerializeField] private TriggerZone _trigger;
    [SerializeField] private ShopContent _contentItems;
    [Space]
    [SerializeField] private TMP_Text _categoryName;
    [Space]
    [SerializeField] private ShopCategotyButton _gpuCategoryButton;
    [SerializeField] private ShopCategotyButton _energyCategoryButton;
    [SerializeField] private ShopCategotyButton _coolingCategoryButton;
    [SerializeField] private ShopCategotyButton _voodooCategoryButton;

    private OpenShopItemChecker _openShopItemChecker;
    private SelectedShopItemChecker _selectedShopItemChecker;

    public InputActionType HideAction => InputActionType.HideStorage;

    public InputActionType[] DisabledActionsOnShow => new[]
    {
        InputActionType.Movement,
        InputActionType.Voodoo,
        InputActionType.FireExtinguisher,
        InputActionType.Interact,
    };

    public InputActionType[] DisabledActionsOnHide => new[]
    {
        InputActionType.HideStorage,
    };

    public bool Init()
    {
        _openShopItemChecker = new(GameManager.Instance.PersistentPlayerData);
        _selectedShopItemChecker = new(GameManager.Instance.PersistentPlayerData);

        _shopPanel.Init(_openShopItemChecker, _selectedShopItemChecker);

        Hide();
        return true;
    }

    private void OnEnable() => SubOnEvents();
    private void OnDisable() => UnsubOnEvents();

    private void OnGPUButtonClick()
    {
        UnselectAllButtons();
        _gpuCategoryButton.Select();
        _categoryName.text = "GPU";
        _shopPanel.Show(_contentItems.GPUItems.Cast<ShopItem>());
    }

    private void OnGeneratorButtonClick()
    {
        UnselectAllButtons();
        _energyCategoryButton.Select();
        _categoryName.text = "Energy";
        _shopPanel.Show(_contentItems.GeneratorItems.Cast<ShopItem>());
    }

    private void OnCoolingButtonClick()
    {
        UnselectAllButtons();
        _coolingCategoryButton.Select();
        _categoryName.text = "Cooling";
        _shopPanel.Show(_contentItems.CoolingsItems.Cast<ShopItem>());
    }

    private void OnVoodooButtonClick()
    {
        UnselectAllButtons();
        _voodooCategoryButton.Select();
        _categoryName.text = "Voodoo";
        _shopPanel.Show(_contentItems.VoodooItems.Cast<ShopItem>());
    }

    private void UnselectAllButtons()
    {
        _gpuCategoryButton.Unselect();
        _energyCategoryButton.Unselect();
        _coolingCategoryButton.Unselect();
        _voodooCategoryButton.Unselect();
    }

    private void ShowStorage()
    {
        UIManager.Instance.Show(this);
    }

    private void SubOnEvents()
    {
        _trigger.Interact += ShowStorage;

        _gpuCategoryButton.Click += OnGPUButtonClick;
        _energyCategoryButton.Click += OnGeneratorButtonClick;
        _coolingCategoryButton.Click += OnCoolingButtonClick;
        _voodooCategoryButton.Click += OnVoodooButtonClick;
    }

    private void UnsubOnEvents()
    {
        _trigger.Interact -= ShowStorage;

        _gpuCategoryButton.Click -= OnGPUButtonClick;
        _energyCategoryButton.Click -= OnGeneratorButtonClick;
        _coolingCategoryButton.Click -= OnCoolingButtonClick;
        _voodooCategoryButton.Click -= OnVoodooButtonClick;
    }

    public void Show() => _storage.SetActive(true);
    public void Hide() => _storage.SetActive(false);
}
