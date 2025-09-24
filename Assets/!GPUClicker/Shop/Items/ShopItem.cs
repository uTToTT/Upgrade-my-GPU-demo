using UnityEngine;

public abstract class ShopItem : ScriptableObject
{
    [SerializeField] private Sprite _sprite;
    [SerializeField, Min(0)] private float _price = 1;

    #region ==== Properties ====

    public Sprite Sprite => _sprite;
    public float Price => _price;

    #endregion =================
}
