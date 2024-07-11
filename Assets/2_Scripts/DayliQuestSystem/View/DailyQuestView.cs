using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DailyQuestView : MonoBehaviour
{
    [SerializeField] private QuestView questViewPrefab;
    [SerializeField] private Transform questContainer;

    private List<QuestView> questViews = new();

    private void Start()
    {
        QuestManager.Instance.TryGetCurrentDailyQuests(out DailyQuests dailyQuest);

        for (int i = 0; i < dailyQuest.Quests.Count; i++)
        {
            InstantiateQuestView(dailyQuest.Quests[i].GetDescription, 
                dailyQuest.Quests[i].GetRewardText, 
                dailyQuest.Quests[i].GetCurrentProgress, 
                dailyQuest.Quests[i].GetMaxProgress,
                dailyQuest.Quests[i].GetIsClaimed,
                dailyQuest.QuestConfigAndEventProvider[i].OnClaim,
                dailyQuest.Quests[i].Claim,
                dailyQuest.Quests[i].OnStateChange);
        }
    }

    private void InstantiateQuestView(string description, string reward, int progress, int maxProgress, bool isClaim, UnityEvent onClaim, Action claim, Action<int, bool> onStateChange)
    {
        QuestView questView = Instantiate(questViewPrefab, questContainer);

        questView.Init(description, reward, progress, maxProgress, isClaim, onClaim, claim, onStateChange);

        questViews.Add(questView);
    }
}
