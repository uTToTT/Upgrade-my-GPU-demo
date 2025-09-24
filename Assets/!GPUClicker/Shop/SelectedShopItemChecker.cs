public class SelectedShopItemChecker : IShopItemVisitor
{
    private IPersistentData _persistentData;

    public bool IsSelected { get; private set; }

    public SelectedShopItemChecker(IPersistentData persistentData) => _persistentData = persistentData;

    public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

    public void Visit(GPUItem GPUItem) =>
        IsSelected = _persistentData.PlayerData.SelectedGPU == GPUItem.GPUType;

    public void Visit(GeneratorItem generatorItem) =>
        IsSelected = _persistentData.PlayerData.SelectedGenerator == generatorItem.GeneratorType;

    public void Visit(CoolingItem coolingItem) =>
        IsSelected = _persistentData.PlayerData.SelectedCooling == coolingItem.CoolingType;

    public void Visit(VoodooItem voodooItem) =>
        IsSelected = _persistentData.PlayerData.SelectedVoodoo == voodooItem.VoodooType;
}
