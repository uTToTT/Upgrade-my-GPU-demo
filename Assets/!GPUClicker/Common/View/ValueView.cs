using NaughtyAttributes;
using System;
using TMPro;
using UnityEngine;

public class ValueView<T> : MonoBehaviour where T : IConvertible
{
    [SerializeField] private TMP_Text _text;

    [SerializeField] private bool _useFormatting = true;

    [ShowIf(nameof(_useFormatting))]
    [SerializeField] private NumberFormat _numberFormat = NumberFormat.Compact;

    public virtual void Show(T value)
    {
        gameObject.SetActive(true);

        string text;

        if (_useFormatting)
        {
            double numericValue = Convert.ToDouble(value);
            text = NumberFormatter.Format(_numberFormat, numericValue);
        }
        else
        {
            text = value.ToString();
        }

        _text.text = text;
    }

    public void Hide() => gameObject.SetActive(false);
}
