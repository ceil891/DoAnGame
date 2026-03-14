using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FruitManager : MonoBehaviour
{
    public static FruitManager Instance;

    public TextMeshProUGUI fruitText;

    public int currentScore;
    public int maxScore;


    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name.StartsWith("Level"))
        {
            ResetScore();
        }

        FindUI();
    }


    // ================= SCORE =================

    public void RegisterFruitScore(int score)
    {
        maxScore += score;
        UpdateUI();
    }


    public void AddScore(int score)
    {
        currentScore += score;
        UpdateUI();
    }

    void ResetScore()
    {
        currentScore = 0;
        maxScore = 0;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (fruitText == null) return;

        fruitText.text = $"{currentScore} / {maxScore}";

        float percent = (float)currentScore / maxScore;

        if (percent < 0.5f)
            fruitText.color = Color.white;
        else if (percent < 1f)
            fruitText.color = Color.yellow;
        else
            fruitText.color = Color.green;
    }

    // ================= STAR =================

    public int GetStarCount()
    {
        if (maxScore <= 0) return 0;

        float percent = (float)currentScore / maxScore;

        if (percent == 1f) return 3;
        if (percent >= 0.7f) return 2;
        if (percent >= 0.3f) return 1;
        return 0;
    }

    void FindUI()
    {
        var obj = GameObject.Find("FruitText (TMP)");
        if (obj != null)
            fruitText = obj.GetComponent<TextMeshProUGUI>();
    }
}
