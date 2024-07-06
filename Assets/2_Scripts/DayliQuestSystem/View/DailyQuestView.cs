using System;
using System.Collections.Generic;
using UnityEngine;

public class DailyQuestView : MonoBehaviour
{
    [SerializeField] private QuestManager questManager;
    [SerializeField] private QuestView questViewPrefab;
    [SerializeField] private Transform questContainer;

    private List<QuestView> questViews = new();

    private void Start()
    {
        questManager.TryGetCurrentDailyQuests(out DailyQuests dailyQuest);


        foreach (Quest item in dailyQuest.Quests)
        {
            InstantiateQuestView(item.GetDescription, "100", item.GetCurrentProgress, item.GetMaxProgress);
        }
    }

    private void InstantiateQuestView(string description, string reward, int progress, int maxProgress)
    {
        QuestView questView = Instantiate(questViewPrefab, questContainer);

        questView.Init(description, reward, progress, maxProgress);

        questViews.Add(questView);
    }
}
