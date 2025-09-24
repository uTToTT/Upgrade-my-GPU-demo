using UnityEngine;

[CreateAssetMenu(fileName = "GeneratorItem", menuName = "Shop/GeneratorItem")]
public class GeneratorItem : ShopItem
{
    [SerializeField] private Generators _genType;

    #region ==== Properties ====

    public Generators GeneratorType => _genType;

    #endregion =================
}
