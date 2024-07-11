using Base.Data;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using static Base.Data.SaveData;

public class FenceOnShop : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthView;
    [SerializeField] private Button upgradeButton;
    [SerializeField] private TextMeshProUGUI upgradeCostView;

    private FenceData data;

    private void Start()
    {
        data = Data.Instance.SaveData.Fence;
        ChangeInfo();
    }

    public void ChangeInfo()
    {
        upgradeButton.onClick.RemoveAllListeners();

        healthView.text = data.FenceHealth.ToString();
        upgradeCostView.text = data.FenceUprgadeCost.ToString();

        upgradeButton.interactable = true;

        upgradeButton.onClick.AddListener(() =>
        {
            if (Data.Instance.SaveData.Money < data.FenceUprgadeCost) return;

            Wallet.Instance.SubstructMoney(data.FenceUprgadeCost);
            Data.Instance.UpgrateFence();
            ChangeInfo();


            if (QuestManager.Instance.TryGetCurrentDailyQuests(out DailyQuests dailyQuest)
                    && dailyQuest.TryGetQuestWithType(QuestType.PumpWall3Times, out Quest quest))
            {
                quest.Increament(1);
            }
        });
    }
}
