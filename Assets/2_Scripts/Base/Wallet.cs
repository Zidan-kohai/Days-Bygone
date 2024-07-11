using Base.Data;
using System;
using UnityEngine;

public class Wallet : MonoBehaviour
{
    public static Wallet Instance;

    public event Action onMoneyChange;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void AddMoney(int money)
    {
        Data.Instance.ChangeMoney(money);
        onMoneyChange?.Invoke();

        if (QuestManager.Instance.TryGetCurrentDailyQuests(out DailyQuests dailyQuest))
        {
            if (dailyQuest.TryGetQuestWithType(QuestType.Earn300Coins, out Quest quest))
            {
                quest.Increament(money);
            }
        }
    }

    public void SubstructMoney(int money)
    {
        Data.Instance.ChangeMoney(-money);
        onMoneyChange?.Invoke();
    }
}
