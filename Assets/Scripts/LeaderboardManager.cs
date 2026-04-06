using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;
public class LeaderboardManager : MonoBehaviour
{
    [Header("UI Nối Dây")]
    public Transform contentContainer; 
    public GameObject playerEntryPrefab; 
    public Button backButton;
    public Button refreshButton;

    [Header("Màu sắc Top 3")]
    public Color firstPlaceColor = new Color(1f, 0.84f, 0f); // Vàng
    public Color secondPlaceColor = new Color(0.75f, 0.75f, 0.75f); // Bạc
    public Color thirdPlaceColor = new Color(0.8f, 0.5f, 0.2f); // Đồng
    public Color defaultColor = new Color(0.2f, 0.2f, 0.2f, 0.5f); // Tối

    public class PlayerData {
        public string name;
        public int score;
        public PlayerData(string n, int s) { name = n; score = s; }
    }

    void Start()
    {
        refreshButton.onClick.AddListener(LoadLeaderboard);
        backButton.onClick.AddListener(CloseLeaderboard);
        LoadLeaderboard(); 
    }

    public void LoadLeaderboard()
    {
        foreach (Transform child in contentContainer) {
            Destroy(child.gameObject);
        }

        // Dữ liệu test giả lập
        List<PlayerData> players = new List<PlayerData> {
            new PlayerData("Dao Quang Thanh", 9999),
            new PlayerData("PixelMaster", 1200),
            new PlayerData("Noob_01", 300),
            new PlayerData("SpeedRunner", 850),
            new PlayerData("Shadow", 920),
            new PlayerData("Ghost", 150)
        };

        // Xếp hạng từ cao xuống thấp
        players = players.OrderByDescending(p => p.score).ToList();

        for (int i = 0; i < players.Count; i++)
        {
            GameObject entryGo = Instantiate(playerEntryPrefab, contentContainer);
            LeaderboardEntry entryScript = entryGo.GetComponent<LeaderboardEntry>();
            
            if (entryScript != null)
            {
                Color rankColor = defaultColor;
                if (i == 0) rankColor = firstPlaceColor;
                else if (i == 1) rankColor = secondPlaceColor;
                else if (i == 2) rankColor = thirdPlaceColor;

                entryScript.Setup(i + 1, players[i].name, players[i].score, rankColor);
            }
        }
    }

void CloseLeaderboard()
    {
        SceneManager.LoadScene("MainMenu"); 
    }
}