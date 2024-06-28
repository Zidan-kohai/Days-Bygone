using Base.Data;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelWindow : MonoBehaviour
{
    [SerializeField] private RectTransform mapItemHandler;
    [SerializeField] private LevelView mapItemPrefab;
    [SerializeField] private List<Level> levels;
    [SerializeField] private List<LevelView> mapItems = new List<LevelView>();
    [SerializeField] private int MaxMapItemOnOneTime;
    [SerializeField] private int currentLastMapItemIndex = 0;

    [SerializeField] private Button rightButton;
    [SerializeField] private Button leftButton;

    private void Start()
    {
        rightButton.onClick.AddListener(Right);
        leftButton.onClick.AddListener(Left);

        SpawnItem(currentLastMapItemIndex);
    }

    private void Right()
    {
        if (currentLastMapItemIndex + MaxMapItemOnOneTime > levels.Count) return;

        currentLastMapItemIndex += MaxMapItemOnOneTime;
        DestroyItems();
        SpawnItem(currentLastMapItemIndex);
    }

    private void Left()
    {
        if (currentLastMapItemIndex - MaxMapItemOnOneTime < 0) return;

        currentLastMapItemIndex -= MaxMapItemOnOneTime;

        DestroyItems();
        SpawnItem(currentLastMapItemIndex);
    }

    private void SpawnItem(int startIndex)
    {
        int i = startIndex;

        while (i < MaxMapItemOnOneTime + startIndex && i < levels.Count)
        {
            LevelView map = Instantiate(mapItemPrefab, mapItemHandler);
            int index = i;
            bool isOpened = index + 1 <= Data.Instance.GetOpenedLevel;
            map.Init(isOpened, index + 1, () => LoadLevel(index + 1));
            mapItems.Add(map);
            i++;
        }
    }

    private void LoadLevel(int levelIndex)
    {
        Data.Instance.CurrentLevel = levelIndex;
        SceneManager.LoadScene(1);
    }

    private void DestroyItems()
    {
        while (mapItems.Count > 0)
        {
            LevelView level = mapItems[0];
            mapItems.RemoveAt(0);
            Destroy(level.gameObject);
        }
    }
}
