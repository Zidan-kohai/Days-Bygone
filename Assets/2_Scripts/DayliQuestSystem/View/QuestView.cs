using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuestView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI reward;
    [SerializeField] private Slider progressSlider;
    [SerializeField] private Button button;

    public void Init(string description, string reward, int maxProgress, int currentProgress)
    {
        this.description.text = description;
        this.reward.text = reward;
        this.progressSlider.maxValue = maxProgress;
        this.progressSlider.value = currentProgress;
    }
}
