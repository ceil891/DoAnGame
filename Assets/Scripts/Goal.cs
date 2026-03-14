using UnityEngine;

public class Goal : MonoBehaviour
{
    private bool triggered = false;

    [Header("Level Info")]
    public int currentLevel = 1;   // Level hiện tại (1,2,3...)

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (triggered) return;

        if (other.CompareTag("Player"))
        {
            triggered = true;

            UnlockNextLevel();
            UIManager.Instance.ShowComplete();
        }
    }

    void UnlockNextLevel()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        // Nếu level hiện tại >= level đã mở → mở level tiếp
        if (currentLevel >= unlockedLevel)
        {
            PlayerPrefs.SetInt("UnlockedLevel", currentLevel + 1);
            PlayerPrefs.Save();
        }
    }
}
