using Base.Data;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponOnShop : MonoBehaviour
{
    [SerializeField] private GameObject closePanel;
    [SerializeField] private TextMeshProUGUI speedView;
    [SerializeField] private TextMeshProUGUI damageView;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TextMeshProUGUI upgradeCostView;
    [SerializeField] private TextMeshProUGUI upgradeTextView;
    [SerializeField] private Image goldIcon;
    [SerializeField] private Button equipButton;
    [SerializeField] private TextMeshProUGUI equipText;
    [SerializeField] private WeaponData data;
    [SerializeField] private int weaponId;
    [SerializeField] private bool isClose;



    public void Init(bool isClose, int weaponId)
    {
        data = Data.Instance.GetWeaponData(weaponId);

        this.weaponId = weaponId;
        this.isClose = isClose;

        ChangeInfo();
    }

    private void ChangeInfo()
    {
        upgradeButton.onClick.RemoveAllListeners();
        equipButton.onClick.RemoveAllListeners();

        speedView.text = data.Speed.ToString();
        damageView.text = data.Damage.ToString();
        upgradeCostView.text = data.UpgrateCost.ToString();


        if(!isClose)
        {
            closePanel.SetActive(false);
            upgradeButton.interactable = true;

            if (data.UpdateTimes >= data.MaxUpdateTimes)
            {
                upgradeTextView.text = "Max";
                upgradeCostView.gameObject.SetActive(false);
                goldIcon.gameObject.SetActive(false);
                upgradeButton.interactable = false;
            }
            else
            {
                upgradeButton.onClick.AddListener(() =>
                {
                    Data.Instance.UpgrateWeapon(weaponId);
                    ChangeInfo();
                });
            }
            

            equipButton.interactable = true;
            equipText.text = "Equip";

            equipButton.onClick.AddListener(() =>
            {
                Data.Instance.CurrentWeaponId = weaponId;
                ChangeInfo();
            });
        }

        if(Data.Instance.CurrentWeaponId == weaponId)
        {
            equipButton.interactable = false;
            equipText.text = "Equiped";
        }
        else if(isClose)
        {
            equipButton.interactable = false;
            upgradeButton.interactable = false;
            closePanel.SetActive(true);
        }
    }
}
