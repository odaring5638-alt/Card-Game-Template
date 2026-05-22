using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public TextMeshProUGUI playerPointsText;
    public TextMeshProUGUI aiPointsText;
    public TextMeshProUGUI playerSweetnessText;
    public TextMeshProUGUI aiSweetnessText;

    void Update()
    {
        playerPointsText.text = "Player Points: " + GameManager.gm.player_points;
        aiPointsText.text = "AI Points: " + GameManager.gm.ai_points;
        playerSweetnessText.text = "Your Cake's Sweetness: " + GameManager.gm.player_sweetness;
        aiSweetnessText.text = "AI Cake's Sweetness: " + GameManager.gm.ai_sweetness;
    }
}
