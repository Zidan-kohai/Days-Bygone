using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "DayliConfig", menuName = "Quest/DailyQuestsConfig")]
public class DailyQuestsConfig : ScriptableObject
{
    [field: SerializeField] public List<QuestConfig> questConfigs { get; private set; }
}