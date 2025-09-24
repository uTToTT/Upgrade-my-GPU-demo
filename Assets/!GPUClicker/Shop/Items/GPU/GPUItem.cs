using UnityEngine;

[CreateAssetMenu(fileName = "GPUItem", menuName = "Shop/GPUItem")]
public class GPUItem : ShopItem
{
    [SerializeField] private GPUs _gpuType;

    #region ==== Properties ====

    public GPUs GPUType => _gpuType;

    #endregion =================
}
