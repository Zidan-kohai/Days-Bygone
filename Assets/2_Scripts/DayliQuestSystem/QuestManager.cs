using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager Instance;

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
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;    
        DontDestroyOnLoad(gameObject);

        timeTracker = new TimeTracker(resetable);

        Load();

        InitDailyQuests();
    }

    private void InitDailyQuests()
    {
        for (int i = 0; i < dailyQuestConfigs.Count; i++)
        {
            DailyQuests dayliQuest = new DailyQuests(this, dailyQuestConfigs[i].questConfigAndEventProvider, dailyQuestData[i]);

            this.dailyQuests.Add(dayliQuest);
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
                List<QuestData> questDatas = new List<QuestData>();
                for (int j = 0; j < 10; j++)
                {
                    QuestData data = new QuestData();
                    questDatas.Add(data);
                }

                emptyList.Add(new DailyQuestsData(questDatas));
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
}