using TMPro;
using UnityEngine;

public class MiningUI : MonoBehaviour
{
    [SerializeField] private TMP_Text _miningInfo;

    private void LateUpdate()
    {
        UpdateUI();
    }

    private void UpdateUI()
    {
        var income = MiningManager.Instance.TotalIncomePerSecond;

        var text =
            $"<b>MINING</b>\n" +
            $"Income:\t{income.ToString("0.####")}\n";

        _miningInfo.text = text;
    }
}
