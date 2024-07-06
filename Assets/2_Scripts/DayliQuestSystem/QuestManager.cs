using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

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
        string json = PlayerPrefs.HasKey(SaveDataKey) ? 
            PlayerPrefs.GetString(SaveDataKey) : 
            JsonConvert.SerializeObject(new List<DailyQuestsData>(dailyQuestData.Count));

        dailyQuestData = JsonConvert.DeserializeObject<List<DailyQuestsData>>(json);
    }

    public void Save()
    {
        string json = JsonConvert.SerializeObject(dailyQuestData);

        PlayerPrefs.SetString(SaveDataKey,json);
    }
}