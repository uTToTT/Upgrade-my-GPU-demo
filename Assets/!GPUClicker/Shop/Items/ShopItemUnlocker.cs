public class ShopItemUnlocker : IShopItemVisitor
{
    private IPersistentData _persistentData;

    public ShopItemUnlocker(IPersistentData persistentData) => _persistentData = persistentData;

    public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

    public void Visit(GPUItem GPUItem) =>
        _persistentData.PlayerData.OpenGPU(GPUItem.GPUType);

    public void Visit(GeneratorItem generatorItem) =>
        _persistentData.PlayerData.OpenGenerator(generatorItem.GeneratorType);

    public void Visit(CoolingItem coolingItem) =>
        _persistentData.PlayerData.OpenCooling(coolingItem.CoolingType);

    public void Visit(VoodooItem voodooItem) =>
        _persistentData.PlayerData.OpenVoodoo(voodooItem.VoodooType);
}
