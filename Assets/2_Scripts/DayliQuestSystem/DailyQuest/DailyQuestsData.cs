using System;
using System.Collections.Generic;

[Serializable]
public class DailyQuestsData
{
    public List<QuestData> QuestDatas = new List<QuestData>();

    public DailyQuestsData()
    {
        for (int i = 0; i < 10; i++)
        { 
            QuestData data = new QuestData();
            QuestDatas.Add(data);
        }
    }
}