using TMPro;
using UnityEngine;

public class EnergyUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _energyInfo;

    private void LateUpdate()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        var production = EnergyManager.Instance.CurrentProduction;
        var ratedConsumption = EnergyManager.Instance.RatedConsumption;
        var consumption = EnergyManager.Instance.CurrentConsumption;
        var netEnergy = EnergyManager.Instance.NetEnergy;

        var text =
            $"<b>ENERGY</b>\n" +
            $"Production:\t{production}\n" +
            $"Rated cons:\t{ratedConsumption}\n" +
            $"Cons:      \t{consumption}\n" +
            $"Net energy:\t{netEnergy}\n";

        _energyInfo.text = text;
    }
}
