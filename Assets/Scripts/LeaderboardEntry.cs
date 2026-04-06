using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LeaderboardEntry : MonoBehaviour
{
    public TextMeshProUGUI rankText;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI scoreText;
    public Image backgroundImage;

    public void Setup(int rank, string playerName, int score, Color highlightColor)
    {
        rankText.text = "#" + rank;
        nameText.text = playerName;
        scoreText.text = score.ToString();
        
        if (backgroundImage != null) {
            backgroundImage.color = highlightColor;
        }
    }
}