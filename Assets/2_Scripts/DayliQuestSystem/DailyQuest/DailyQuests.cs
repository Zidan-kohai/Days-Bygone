using System;
using System.Collections.Generic;

public class DailyQuests
{
    private readonly QuestManager questManager;
    private List<QuestConfig> questConfigs = new();
    private DailyQuestsData data;

    public List<Quest> Quests { get; private set; }

    public DailyQuests(QuestManager questManager, List<QuestConfig> questConfigs, DailyQuestsData data)
    {
        this.questManager = questManager;
        this.questConfigs = questConfigs;
        this.data = data;

        InitQuest();
    }

    private void InitQuest()
    {
        for (int i = 0; i < questConfigs.Count; i++)
        {
            Quest quest = new Quest(questManager, questConfigs[i], data.QuestDatas[i]);
            Quests.Add(quest);
        }
    }

    public bool TryGetQuestWithType(QuestType questType, out Quest quest)
    {
        quest = null;

        if(Quests.Exists(a => a.GetQuestType == questType))
        {
            return false;
        }

        quest = Quests.Find(a => a.GetQuestType == questType);
        return true;
    }
}