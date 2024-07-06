using System;
using System.Security.Claims;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QuestView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI description;
    [SerializeField] private TextMeshProUGUI reward;
    [SerializeField] private Slider progressSlider;
    [SerializeField] private Button button;

    public void Init(string description, string reward, int currentProgress, int maxProgress, bool isClaim, ref UnityEvent onClaim, Action claim, ref Action<int, bool> onStateChange)
    {
        this.description.text = description;
        this.reward.text = reward;
        this.progressSlider.maxValue = maxProgress;
        this.progressSlider.value = currentProgress;
        onStateChange += OnStateChage;
        onClaim.AddListener(DisableButton);

        if (isClaim || currentProgress < maxProgress)
        {
            DisableButton();
        }
        else
        {
            EnableButton();
            button.onClick.AddListener(() => claim?.Invoke());
        }
    }

    private void DisableButton()
    {
        button.interactable = false;
    }

    private void EnableButton()
    {
        button.interactable = true;
    }

    private void OnStateChage(int progress, bool canClaim)
    {
        progressSlider.value = progress;

        if(canClaim)
        {
            EnableButton();
        }
        else
        {
            DisableButton();
        }
    }
}
