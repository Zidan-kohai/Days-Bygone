using Unit.Enemy.Base;

namespace Base.Spawner
{
    public class BigGoblin : Enemy
    {
        protected override void OnDeath()
        {
            base.OnDeath();

            if (QuestManager.Instance.TryGetCurrentDailyQuests(out DailyQuests dailyQuest))
            {
                if (dailyQuest.TryGetQuestWithType(QuestType.KillOneBoss, out Quest quest))
                {
                    quest.Increament(1);
                }
            }
        }
    }
}