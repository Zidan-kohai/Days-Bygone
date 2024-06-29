using Base.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWindowController : MonoBehaviour
{
    [SerializeField] private GameObject WinPanel;
    [SerializeField] private GameObject LosePanel;
    [SerializeField] private GameObject PausePanel;

    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    public void PauseOn()
    {
        Time.timeScale = 0.0f;
        PausePanel.SetActive(true);
    }

    public void PauseOff()
    {
        Time.timeScale = 1.0f;
        PausePanel.SetActive(false);
    }

    public void Win()
    {
        TryOpenNewLevel();
        Time.timeScale = 0.0f;
        WinPanel.SetActive(true);
    }

    public void Lose()
    {
        Time.timeScale = 0.0f;
        LosePanel.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void TryOpenNewLevel()
    {
        if (Data.Instance.GetOpenedLevel == Data.Instance.CurrentLevel)
        {
            Data.Instance.SetOpenLevel();
        }

        Data.Instance.CurrentLevel++;
    }
}
