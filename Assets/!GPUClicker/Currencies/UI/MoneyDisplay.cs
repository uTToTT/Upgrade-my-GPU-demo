using TMPro;
using UnityEngine;
using UTToTTGames.Initialization;

public class MoneyDisplay : MonoBehaviour, IInitializable
{
    [SerializeField] private TMP_Text _moneyText;

    private float _money;

    public bool Init()
    {
        GameManager.Instance.Wallet.OnMoneyChanged += UpdateAmount;

        return true;
    }

    private void UpdateAmount(float currAmount)
    {
        _money = currAmount;
    }

    public void UpdateDisplay()
    {
        string text = $"Money: {_money.ToString("0.####")}";
        _moneyText.text = text;
    }
}
