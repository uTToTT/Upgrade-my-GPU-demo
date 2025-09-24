
public interface IShopItemVisitor
{
    void Visit(ShopItem item);
    void Visit(GPUItem item);
    void Visit(GeneratorItem item);
    void Visit(CoolingItem item);
    void Visit(VoodooItem item);

}
