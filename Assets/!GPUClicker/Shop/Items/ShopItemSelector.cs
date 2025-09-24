public class ShopItemSelector : IShopItemVisitor
{
    private IPersistentData _persistentData;

    public ShopItemSelector(IPersistentData persistentData) => _persistentData = persistentData;

    public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

    public void Visit(GPUItem GPUItem) => 
        _persistentData.PlayerData.SelectedGPU = GPUItem.GPUType;

    public void Visit(GeneratorItem generatorItem) =>
        _persistentData.PlayerData.SelectedGenerator = generatorItem.GeneratorType;

    public void Visit(CoolingItem coolingItem) =>
        _persistentData.PlayerData.SelectedCooling = coolingItem.CoolingType;

    public void Visit(VoodooItem voodooItem) =>
        _persistentData.PlayerData.SelectedVoodoo = voodooItem.VoodooType;
}
