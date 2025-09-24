using System.Linq;

public class OpenShopItemChecker : IShopItemVisitor
{

    private IPersistentData _persistentData;

    public bool IsOpened { get; private set; }  

    public OpenShopItemChecker(IPersistentData persistentData) => _persistentData = persistentData;

    public void Visit(ShopItem shopItem) => Visit((dynamic)shopItem);

    public void Visit(GPUItem GPUItem) =>
        IsOpened = _persistentData.PlayerData.OpenGPUs.Contains(GPUItem.GPUType);

    public void Visit(GeneratorItem generatorItem) =>
        IsOpened = _persistentData.PlayerData.OpenGenerators.Contains(generatorItem.GeneratorType);

    public void Visit(CoolingItem coolingItem) =>
        IsOpened = _persistentData.PlayerData.OpenCoolings.Contains(coolingItem.CoolingType);

    public void Visit(VoodooItem voodooItem) =>
        IsOpened = _persistentData.PlayerData.OpenVoodoos.Contains(voodooItem.VoodooType);
}
