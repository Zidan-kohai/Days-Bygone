using Base.Data;
using UnityEngine;
using UnityEngine.EventSystems;

public class BulletDrop : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private int id;

    public void OnPointerClick(PointerEventData eventData)
    {
        Data.Instance.OpenWeapon(id);
        Debug.Log("Click on Drop weapon with id:" + id);
        Destroy(gameObject);

        if (QuestManager.Instance.TryGetCurrentDailyQuests(out DailyQuests dailyQuest))
        {
            if (dailyQuest.TryGetQuestWithType(QuestType.FindOneWeapon, out Quest quest))
            {
                quest.Increament(1);
            }
        }
    }
}