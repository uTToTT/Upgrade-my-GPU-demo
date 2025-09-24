using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class ShopItemView : MonoBehaviour, IPointerClickHandler
{
    public event Action<ShopItemView> Click;

    [SerializeField] private Sprite _standartBackground;
    [SerializeField] private Sprite _highlightBackground;

    [SerializeField] private Image _contentImage;
    [SerializeField] private Image _lockImage;

    [SerializeField] private FloatValueView _priceView;

    [SerializeField] private Image _selectionText;

    private Image _backgroundImage;

    public ShopItem Item { get; private set; }
    public bool IsLock {  get; private set; }

    public  float Price => Item.Price;

    public Sprite Sprite => Item.Sprite;

    public void Init(ShopItem item)
    {
        _backgroundImage = GetComponent<Image>();
        _backgroundImage.sprite = _standartBackground;

        Item = item;

        _contentImage.sprite = item.Sprite;
        _priceView.Show(Price);
    }

    public void OnPointerClick(PointerEventData eventData) => Click?.Invoke(this);

    public void Lock()
    {
        IsLock = true;
        _lockImage.gameObject.SetActive(IsLock);
        _priceView.Show(Price);
    }

    public void Unlock()
    {
        IsLock = false;
        _lockImage.gameObject.SetActive(IsLock);
        _priceView.Hide();
    }

    public void Select() => _selectionText.gameObject.SetActive(true);
    public void Unselect() => _selectionText.gameObject.SetActive(false);

    public void Highlight() => _backgroundImage.sprite = _highlightBackground;
    public void Unhighlight() => _backgroundImage.sprite = _standartBackground;

}
