using Base.Data;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WeaponOnShop : MonoBehaviour
{
    [SerializeField] private GameObject closePanel;
    [SerializeField] private TextMeshProUGUI reloadSpeedView;
    [SerializeField] private TextMeshProUGUI bulletSpeedView;
    [SerializeField] private TextMeshProUGUI damageView;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TextMeshProUGUI upgradeCostView;
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

    public void ChangeInfo()
    {
        upgradeButton.onClick.RemoveAllListeners();
        equipButton.onClick.RemoveAllListeners();

        bulletSpeedView.text = data.BulletSpeed.ToString();
        reloadSpeedView.text = data.SpawnSpeed.ToString();
        upgradeCostView.text = data.UpgrateCost.ToString();
        damageView.text = data.Damage.ToString();


        if(!isClose)
        {
            closePanel.SetActive(false);
            upgradeButton.interactable = true;

            if (data.UpdateTimes >= data.MaxUpdateTimes)
            {
                upgradeCostView.text = "Max";
                goldIcon.gameObject.SetActive(false);
                upgradeButton.interactable = false;
            }
            else
            {
                upgradeButton.onClick.AddListener(() =>
                {
                    if (Data.Instance.SaveData.Money < data.UpgrateCost) return;

                    Wallet.Instance.SubstructMoney(data.UpgrateCost);
                    Data.Instance.UpgrateWeapon(weaponId);
                    ChangeInfo();

                    if (QuestManager.Instance.TryGetCurrentDailyQuests(out DailyQuests dailyQuest)
                    && dailyQuest.TryGetQuestWithType(QuestType.UpgradeOneWeapon, out Quest quest))
                    {
                        quest.Increament(1);
                    }
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
