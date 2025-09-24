using System;

public class Wallet
{
    public event Action<float> OnMoneyChanged;

    private IPersistentData _persistentData;

    public Wallet(IPersistentData persistentData)
        => _persistentData = persistentData;

    public void AddMoney(float money)
    {
        if (money < 0)
            throw new ArgumentOutOfRangeException(nameof(money));

        _persistentData.PlayerData.Money += money;

        OnMoneyChanged?.Invoke(_persistentData.PlayerData.Money);
    }

    public float GetCurrentMoney() => _persistentData.PlayerData.Money;

    public bool IsEnough(float money)
    {
        if(money < 0)
            throw new ArgumentOutOfRangeException(nameof(money));

        return _persistentData.PlayerData.Money >= money;
    }

    public void Spend(float money)
    {
        if (money < 0)
            throw new ArgumentOutOfRangeException(nameof(money)); 

        _persistentData.PlayerData.Money -= money;

        OnMoneyChanged?.Invoke(_persistentData.PlayerData.Money);
    }
}
