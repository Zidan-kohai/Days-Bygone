using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelView : MonoBehaviour
{
    [SerializeField] private Button mainButton;
    [SerializeField] private GameObject closePanel;
    [SerializeField] private TextMeshProUGUI textView;

    public void Init(bool isOpen, int levelIndex, Action onClick)
    {
        textView.text = levelIndex.ToString();

        if (!isOpen) return;

        mainButton.interactable = true;
        closePanel.SetActive(false);
        mainButton.onClick.AddListener(() => onClick?.Invoke());
    }
}
