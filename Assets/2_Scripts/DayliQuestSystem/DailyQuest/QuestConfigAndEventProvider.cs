using System;
using UnityEngine.Events;

[Serializable]
public class QuestConfigAndEventProvider
{
    public QuestConfig QuestConfig;
    public string RewardText;
    public UnityEvent OnClaim;
}