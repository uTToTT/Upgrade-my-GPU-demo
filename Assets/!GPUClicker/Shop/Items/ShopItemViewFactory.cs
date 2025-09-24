using System;
using UnityEngine;

[CreateAssetMenu(fileName = "ShopItemViewFactory", menuName = "Shop/ShopItemViewFactory")]
public class ShopItemViewFactory : ScriptableObject
{
    [SerializeField] private ShopItemView _gpuItemPrefab;
    [SerializeField] private ShopItemView _generatorItemPrefab;
    [SerializeField] private ShopItemView _coolingItemPrefab;
    [SerializeField] private ShopItemView _voodooItemPrefab;

    public ShopItemView Get(ShopItem shopItem, Transform parent)
    {
        ShopItemVisitor visitor = new ShopItemVisitor(
            _gpuItemPrefab,
            _generatorItemPrefab,
            _coolingItemPrefab,
            _voodooItemPrefab);

        visitor.Visit(shopItem);

        ShopItemView instance = Instantiate(visitor.Prefab, parent);
        instance.Init(shopItem);

        return instance;
    }

    private class ShopItemVisitor : IShopItemVisitor
    {
        private ShopItemView _gpuItemPrefab;
        private ShopItemView _generatorItemPrefab;
        private ShopItemView _coolingItemPrefab;
        private ShopItemView _voodooItemPrefab;

        public ShopItemVisitor(
            ShopItemView gpuItemPrefab,
            ShopItemView generatorItemPrefab,
            ShopItemView coolingItemPrefab,
            ShopItemView voodooItemPrefab)
        {
            _gpuItemPrefab = gpuItemPrefab;
            _generatorItemPrefab = generatorItemPrefab;
            _coolingItemPrefab = coolingItemPrefab;
            _voodooItemPrefab = voodooItemPrefab;
        }

        public ShopItemView Prefab { get; private set; }

        public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);
        public void Visit(GPUItem GPUItem) =>
            Prefab = _gpuItemPrefab;

        public void Visit(GeneratorItem generatorItem) =>
            Prefab = _generatorItemPrefab;

        public void Visit(CoolingItem coolingItem) =>
            Prefab = _coolingItemPrefab;

        public void Visit(VoodooItem voodooItem) =>
            Prefab = _voodooItemPrefab;
    }
}
