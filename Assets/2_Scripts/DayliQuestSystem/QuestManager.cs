using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private List<DailyQuestsConfig> dailyQuestConfigs = new();
    [SerializeField] private bool resetable;

    private List<DailyQuests> dailyQuests = new();
    private TimeTracker timeTracker;
    
    #region SaveData
    private const string SaveDataKey = "DailyQuestSaveDataKey";
    private List<DailyQuestsData> dailyQuestData = new();
    #endregion

    [SerializeField] private List<DailyEvent> dailyEvents = new();

    private void OnValidate()
    {
        List<DailyEvent> newDailyEvents = new();

        if(dailyQuests.Count != dailyEvents.Count)
        {
            foreach (DailyQuests dailyQuest in dailyQuests)
            {
                DailyEvent dailyEvent = new DailyEvent();

                foreach (Quest quest in dailyQuest.Quests)
                {
                    QuestEvent questEvent = new QuestEvent();
                    questEvent.QuestName = quest.GetQuestType.ToString();

                    dailyEvent.QuestEvent.Add(questEvent);
                }

                newDailyEvents.Add(dailyEvent);
            }
        }

        for (int i = 0; i < dailyQuests.Count; i++)
        {
            foreach (DailyEvent dailyEvent in dailyEvents)
            {
                foreach (QuestEvent questEvent in dailyEvents[i].QuestEvent)
                {
                    newDailyEvents[i].QuestEvent.Add(questEvent);
                }
            }

            if (this.dailyEvents[i].QuestEvent.Count < dailyQuests[i].Quests.Count)
            {
                
            }
        }
    }

    private void Awake()
    {
        timeTracker = new TimeTracker(resetable);

        Load();

        InitDailyQuests();
    }

    private void InitDailyQuests()
    {
        for (int i = 0; i < dailyQuestConfigs.Count; i++)
        {
            DailyQuests dailyCuest = new DailyQuests(this, dailyQuestConfigs[i].questConfigs, dailyQuestData[i]);

            this.dailyQuests.Add(dailyCuest);
        }
    }

    public bool TryGetCurrentDailyQuests(out DailyQuests dailyQuest)
    {
        dailyQuest = null; 

        if (dailyQuests.Count <= timeTracker.GetDayCount)
        {
            Debug.Log($"Have`t Quest for {timeTracker.GetDayCount} day");
            return false;
        }

        dailyQuest = dailyQuests[timeTracker.GetDayCount];
        return true;
    }

    private void Load()
    {
        string json;

        if (PlayerPrefs.HasKey(SaveDataKey))
        {
            json = PlayerPrefs.GetString(SaveDataKey);
        }
        else
        {
            List<DailyQuestsData> emptyList = new List<DailyQuestsData>();
            for (int i = 0; i < dailyQuestConfigs.Count; i++)
            {
                emptyList.Add(new DailyQuestsData());
            }

            json = JsonConvert.SerializeObject(emptyList);
        }

        dailyQuestData = JsonConvert.DeserializeObject<List<DailyQuestsData>>(json);
    }

    public void Save()
    {
        string json = JsonConvert.SerializeObject(dailyQuestData);

        PlayerPrefs.SetString(SaveDataKey,json);
    }

    [Serializable]
    public class DailyEvent
    {
        public string DayName;
        public List<QuestEvent> QuestEvent = new();
    }

    [Serializable]
    public class QuestEvent
    {
        public string QuestName;
        public UnityAction OnClaim;
    }
}