using UnityEngine;

[CreateAssetMenu(fileName = "VoodooItem", menuName = "Shop/VoodooItem")]
public class VoodooItem : ShopItem
{
    [SerializeField] private Voodoos _voodooType;

    #region ==== Properties ====

    public Voodoos VoodooType => _voodooType;

    #endregion =================
}
