using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour, IClosableUI
{
    [SerializeField] private GameObject _shop;
    [Space]
    [SerializeField] private ShopContent _contentItems;
    [Space]
    [SerializeField] private TMP_Text _categoryName;
    [Space]
    [SerializeField] private TriggerZone _gpuShopTrigger;
    [SerializeField] private TriggerZone _energyShopTrigger;
    [SerializeField] private TriggerZone _coolingShopTrigger;
    [SerializeField] private TriggerZone _voodooShopTrigger;
    [Space]
   
    [Space]
    [SerializeField] private BuyButton _buyButton;
    [SerializeField] private Button _selectionButton;
    [SerializeField] private Image _selectedText;
    [Space]
    [SerializeField] private ShopPanel _shopPanel;

    private ShopItemView _previewItem;

    private Wallet _wallet;

    private ShopItemSelector _shopItemSelector;
    private ShopItemUnlocker _shopItemUnlocker;
    private OpenShopItemChecker _openShopItemChecker;
    private SelectedShopItemChecker _selectedShopItemChecker;

    public InputActionType HideAction => InputActionType.HideShop;
    public InputActionType[] DisabledActionsOnShow => new[]
    {
        InputActionType.Voodoo,
        InputActionType.Movement,
        InputActionType.FireExtinguisher,
        InputActionType.Interact,
    };

    public InputActionType[] DisabledActionsOnHide => new[]
    {
        InputActionType.HideShop,
    };

    #region ==== Init ====

    public void Init(
       Wallet wallet,
       OpenShopItemChecker openShopItemChecker,
       SelectedShopItemChecker selectedShopItemChecker,
       ShopItemSelector shopItemSelector,
       ShopItemUnlocker shopItemUnlocker)
    {
        _wallet = wallet;
        _openShopItemChecker = openShopItemChecker;
        _selectedShopItemChecker = selectedShopItemChecker;
        _shopItemSelector = shopItemSelector;
        _shopItemUnlocker = shopItemUnlocker;

        _shopPanel.Init(openShopItemChecker, selectedShopItemChecker);

        Hide();
    }

    #endregion ===========

    #region ==== Unity API ====

    private void OnEnable()
    {
        _gpuShopTrigger.Interact += OnGPUShopShow;
        _energyShopTrigger.Interact += OnGeneratorShopShow;
        _coolingShopTrigger.Interact += OnCoolingShopShow;
        _voodooShopTrigger.Interact += OnVoodooShopShow;

        _shopPanel.ItemViewClicked += OnItemViewClicked;

        _buyButton.Click += OnBuyButtonClick;
        _selectionButton.onClick.AddListener(OnSelectionButtonClick);
    }

    private void OnDisable()
    {
        _gpuShopTrigger.Interact -= OnGPUShopShow;
        _energyShopTrigger.Interact -= OnGeneratorShopShow;
        _coolingShopTrigger.Interact -= OnCoolingShopShow;
        _voodooShopTrigger.Interact -= OnVoodooShopShow;

        _shopPanel.ItemViewClicked -= OnItemViewClicked;

        _buyButton.Click -= OnBuyButtonClick;
        _selectionButton.onClick.RemoveListener(OnSelectionButtonClick);
    }

    #endregion ================

    private void OnItemViewClicked(ShopItemView itemView)
    {
        _previewItem = itemView;

        _openShopItemChecker.Visit(_previewItem.Item);

        if (_openShopItemChecker.IsOpened)
        {
            _selectedShopItemChecker.Visit(_previewItem.Item);

            if (_selectedShopItemChecker.IsSelected)
            {
                ShowSelectedText();
                return;
            }

            ShowSelectionButton();
        }
        else
        {
            ShowBuyButton(_previewItem.Price);
        }
    }

    private void OnBuyButtonClick()
    {
        if (_wallet.IsEnough(_previewItem.Price))
        {
            _wallet.Spend(_previewItem.Price);
            _shopItemUnlocker.Visit(_previewItem.Item);
            SelectItem();

            _previewItem.Unlock();

            GameManager.Instance.SaveGame();
        }
    }

    private void OnSelectionButtonClick()
    {
        SelectItem();

        GameManager.Instance.SaveGame();
    }

    private void OnGPUShopShow()
    {
        UIManager.Instance.Show(this);
        _categoryName.text = "GPU";
        _shopPanel.Show(_contentItems.GPUItems.Cast<ShopItem>());
    }

    private void OnGeneratorShopShow()
    {
        UIManager.Instance.Show(this);
        _categoryName.text = "Energy";
        _shopPanel.Show(_contentItems.GeneratorItems.Cast<ShopItem>());
    }

    private void OnCoolingShopShow()
    {
        UIManager.Instance.Show(this);
        _categoryName.text = "Cooling";
        _shopPanel.Show(_contentItems.CoolingsItems.Cast<ShopItem>());
    }

    private void OnVoodooShopShow()
    {
        UIManager.Instance.Show(this);
        _categoryName.text = "Voodoo";
        _shopPanel.Show(_contentItems.VoodooItems.Cast<ShopItem>());
    }

    private void SelectItem()
    {
        _shopItemSelector.Visit(_previewItem.Item);
        _shopPanel.Select(_previewItem);
        ShowSelectedText();
    }

    #region ==== Activity ====

    private void ShowSelectionButton()
    {
        _selectionButton.gameObject.SetActive(true);
        HideBuyButton();
        HideSelectedText();
    }

    private void ShowSelectedText()
    {
        _selectedText.gameObject.SetActive(true);
        HideSelectionButton();
        HideBuyButton();
    }

    private void ShowBuyButton(float price)
    {
        _buyButton.gameObject.SetActive(true);
        _buyButton.UpdateText(price);

        if (_wallet.IsEnough(price))
            _buyButton.Unlock();
        else
            _buyButton.Lock();

        HideSelectedText();
        HideSelectionButton();
    }

    private void HideBuyButton() => _buyButton.gameObject.SetActive(false);
    private void HideSelectionButton() => _selectionButton.gameObject.SetActive(false);
    private void HideSelectedText() => _selectedText.gameObject.SetActive(false);

    public void Show()
    {
        _shop.SetActive(true);

        //_gpuShopTrigger.SetActiveActivator(false);
        //_energyShopTrigger.SetActiveActivator(false);
        //_coolingShopTrigger.SetActiveActivator(false);
        //_voodooShopTrigger.SetActiveActivator(false);

        //PlayerInputManager.Instance.DisableAll();
        //PlayerInputManager.Instance.SetEnabled(InputActionType.HideShop, true);
    }

    public void Hide()
    {
        _shop.SetActive(false);

        //DelayedExecutor.Instance.Execute(0.25f, TimeType.Scaled, () =>
        //{
        //    _gpuShopTrigger.SetActiveActivator(true);
        //    _energyShopTrigger.SetActiveActivator(true);
        //    _coolingShopTrigger.SetActiveActivator(true);
        //    _voodooShopTrigger.SetActiveActivator(true);
        //});

        //PlayerInputManager.Instance.EnableAll();
        //PlayerInputManager.Instance.SetEnabled(InputActionType.HideShop, false);
    }

    #endregion ===============
}
