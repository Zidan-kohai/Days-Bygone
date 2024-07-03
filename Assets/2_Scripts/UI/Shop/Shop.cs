using Base.Data;
using Randoms.DailyReward;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    [SerializeField] private Button rightButton;
    [SerializeField] private Button leftButton;

    [SerializeField] private List<WeaponOnShop> weapons;
    [SerializeField] private int currentWeaponIndex = 0;


    private void Start()
    {
        for(int i = 0;  i < weapons.Count; i++)
        {
            bool isClose = !Data.Instance.SaveData.OpenWeaponId.Contains(i);
            weapons[i].Init(isClose, i);
        }
        rightButton.onClick.AddListener(RightButtonClick);
        leftButton.onClick.AddListener(LeftButtonClick);

    }

    public void AddMoney(int money)
    {
        Wallet.Instance.AddMoney(money);
    }

    private void RightButtonClick()
    {
        if (currentWeaponIndex >= weapons.Count - 1) return;

        currentWeaponIndex++;

        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].gameObject.SetActive(false);

            if(i == currentWeaponIndex)
            {
                weapons[i].gameObject.SetActive(true);
                weapons[i].ChangeInfo();
            }
        }
    }

    private void LeftButtonClick()
    {
        if (currentWeaponIndex <= 0) return;

        currentWeaponIndex--;

        for (int i = 0; i < weapons.Count; i++)
        {
            weapons[i].gameObject.SetActive(false);

            if (i == currentWeaponIndex)
            {
                weapons[i].gameObject.SetActive(true);
                weapons[i].ChangeInfo();
            }
        }
    }

}
