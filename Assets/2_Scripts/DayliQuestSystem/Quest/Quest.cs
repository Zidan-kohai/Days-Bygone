using System;
using UnityEngine;
using UnityEngine.Events;
public class Quest
{
    public event Action<int, bool> OnStateChange;
    public event Action OnClaim;

    private readonly QuestManager questManager;
    private QuestConfig config;

    private QuestData data;

    public QuestType GetQuestType => config.QuestType;
    public int GetMaxProgress => config.MaxProgress;
    public int GetCurrentProgress => data.Progress;
    public string GetDescription => config.Description;

    public Quest (QuestManager questManager, QuestConfig config, UnityEvent onClaim, QuestData data)
    {
        this.questManager = questManager;
        this.config = config;
        this.data = data;
        this.OnClaim = () => onClaim?.Invoke();
    }

    public void Increament(int delta)
    {
        data.Progress += delta;

        data.Progress = Mathf.Clamp(data.Progress, 0, config.MaxProgress + 1);

        bool canClaimed = !data.IsClaimed && data.Progress >= config.MaxProgress;

        OnStateChange?.Invoke(data.Progress, canClaimed);

        questManager.Save();
    }

    public void Claim()
    {
        if (data.Progress < config.MaxProgress) return;

        data.IsClaimed = true;

        OnClaim?.Invoke();

        questManager.Save();
    }
}
