using System;
using System.Collections.Generic;
using UnityEngine;

public class ShopPanel : MonoBehaviour
{
    public event Action<ShopItemView> ItemViewClicked;

    private List<ShopItemView> _shopItems = new();

    [SerializeField] private Transform _itemsParent;
    [SerializeField] private ShopItemViewFactory _shopItemViewFactory;

    private OpenShopItemChecker _openShopItemChecker;
    private SelectedShopItemChecker _selectedShopItemChecker;

    public void Init(OpenShopItemChecker openShopItemChecker, SelectedShopItemChecker selectedShopItemChecker)
    {
        _openShopItemChecker = openShopItemChecker;
        _selectedShopItemChecker = selectedShopItemChecker;
    }

    public void Show(IEnumerable<ShopItem> items)
    {
        Clear();

        foreach (ShopItem item in items)
        {
            ShopItemView spawnedItem = _shopItemViewFactory.Get(item, _itemsParent);

            spawnedItem.Click += OnItemViewClick;

            spawnedItem.Unselect();
            spawnedItem.Unhighlight();

            _openShopItemChecker.Visit(spawnedItem.Item);

            if (_openShopItemChecker.IsOpened)
            {
                _selectedShopItemChecker.Visit(spawnedItem.Item);

                if (_selectedShopItemChecker.IsSelected)
                {
                    spawnedItem.Select();
                    spawnedItem.Highlight();
                    ItemViewClicked?.Invoke(spawnedItem);
                }

                spawnedItem.Unlock();
            }
            else
            {
                spawnedItem.Lock();
            }

            _shopItems.Add(spawnedItem);
        }
    }

    public void Select(ShopItemView itemView)
    {
        foreach (var item in _shopItems)
            item.Unselect();

        itemView.Select();
    }

    private void OnItemViewClick(ShopItemView itemView)
    {
        Highlight(itemView);
        ItemViewClicked?.Invoke(itemView);
    }

    private void Highlight(ShopItemView itemView)
    {
        foreach (var item in _shopItems)
            item.Unhighlight();

        itemView.Highlight();
    }

    private void Clear()
    {
        foreach (ShopItemView item in _shopItems)
        {
            item.Click -= OnItemViewClick;
            Destroy(item.gameObject);
        }

        _shopItems.Clear();
    }
}
