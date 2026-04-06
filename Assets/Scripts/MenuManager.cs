using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [Header("Character Select")]
    public GameObject[] characters;
    public GameObject[] selectArrows;

    [Header("Level Select (UI Button)")]
    public LevelUIButton[] levelButtons;

    private int selectedCharacter = -1;
    private int unlockedLevel = 1;

    [System.Serializable]
    public class LevelUIButton
    {
        public Button button;
        public Image background;
    }

    void Start()
    {
        // ===== CHARACTER =====
        foreach (var arrow in selectArrows)
            arrow.SetActive(false);

        selectedCharacter = PlayerPrefs.GetInt("SelectedCharacter", 0);

        if (selectedCharacter < 0 || selectedCharacter >= selectArrows.Length)
            selectedCharacter = 0;

        PlayerPrefs.SetInt("SelectedCharacter", selectedCharacter);
        ShowArrow(selectedCharacter);

        // ===== LEVEL =====
        unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);
        UpdateLevelLock();
    }

    // ============ CHARACTER ============
    public void SelectCharacter(int index)
    {
        selectedCharacter = index;
        PlayerPrefs.SetInt("SelectedCharacter", index);
        ShowArrow(index);
    }

    void ShowArrow(int index)
    {
        for (int i = 0; i < selectArrows.Length; i++)
            selectArrows[i].SetActive(false);

        selectArrows[index].SetActive(true);
    }

    // ============ LEVEL ============
    void UpdateLevelLock()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            bool unlocked = (i + 1) <= unlockedLevel;

            levelButtons[i].button.interactable = unlocked;

            // Background
            if (levelButtons[i].background != null)
                levelButtons[i].background.color = unlocked ? Color.white : Color.gray;

            // Auto find TMP label
            TMP_Text label = levelButtons[i].button.GetComponentInChildren<TMP_Text>();
            if (label != null)
                label.color = unlocked ? Color.white : Color.gray;
        }
    }

    public void SelectLevel(int levelIndex)
    {
        if (selectedCharacter == -1)
        {
            Debug.Log("Chưa chọn nhân vật!");
            return;
        }

        if (levelIndex > unlockedLevel)
        {
            Debug.Log("Level chưa mở!");
            return;
        }

        SceneManager.LoadScene("Level" + levelIndex);
    }

    // ============ MENU ============
    public void GoToLevelSelect()
    {
        SceneManager.LoadScene("LevelSelect");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void BackToMenu()
    {
        // Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void GoToRankScene()
{
    UnityEngine.SceneManagement.SceneManager.LoadScene("Rank");
}
}

