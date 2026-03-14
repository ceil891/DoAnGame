using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;

    [Header("Panels")]
    public GameObject completePanel;
    public GameObject pausePanel;

    [Header("Stars")]
    public Image star1;
    public Image star2;
    public Image star3;

    bool isPaused = false;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // ================= COMPLETE =================
    public void ShowComplete()
    {
        completePanel.SetActive(true);
        Time.timeScale = 0f;

        int stars = FruitManager.Instance.GetStarCount();

        star1.color = stars >= 1 ? Color.white : Color.gray;
        star2.color = stars >= 2 ? Color.white : Color.gray;
        star3.color = stars >= 3 ? Color.white : Color.gray;
    }

    public void NextLevel()
    {
        Time.timeScale = 1f;

        string currentScene = SceneManager.GetActiveScene().name;
        int currentLevel = int.Parse(currentScene.Replace("Level", ""));
        int nextLevel = currentLevel + 1;

        int unlocked = PlayerPrefs.GetInt("UnlockedLevel", 1);
        if (nextLevel > unlocked)
            PlayerPrefs.SetInt("UnlockedLevel", nextLevel);

        if (Application.CanStreamedLevelBeLoaded("Level" + nextLevel))
            SceneManager.LoadScene("Level" + nextLevel);
        else
            SceneManager.LoadScene("LevelSelect");
    }

    // ================= PAUSE =================
    public void PauseGame()
    {
        if (isPaused) return;

        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    // ================= MENU =================
    public void BackToMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
