using Base.Data;
using TMPro;
using UnityEngine;

public class MoneyView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI moneyView;

    private void Start()
    {
        Wallet.Instance.onMoneyChange += ChangeInfo;

        ChangeInfo();
    }

    private void ChangeInfo()
    {
        moneyView.text = Data.Instance.SaveData.Money.ToString();
    }


    private void OnDestroy()
    {
        Wallet.Instance.onMoneyChange -= ChangeInfo;
    }
}
