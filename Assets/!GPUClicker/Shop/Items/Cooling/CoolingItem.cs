using UnityEngine;

[CreateAssetMenu(fileName = "CoolingItem", menuName = "Shop/CoolingItem")]
public class CoolingItem : ShopItem
{
    [SerializeField] private Coolings _coolType;

    #region ==== Properties ====

    public Coolings CoolingType => _coolType;

    #endregion =================

}
