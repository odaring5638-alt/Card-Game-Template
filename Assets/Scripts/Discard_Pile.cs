using System.Collections.Generic;
using UnityEngine;

public class Discard_Pile : MonoBehaviour
{
    public int card_count = 0;
    public List<Card_Data> discarded_cards = new List<Card_Data>();

    public void DiscardCard(Card card)
    {
        if (GameManager.gm.player_points < card.data.cost)
        {
            Debug.Log("Not enough points to play this card!");
            card.transform.SetParent(GameManager.gm.canvas.transform, false);
            return;
        }

        discarded_cards.Add(card.data);
        GameManager.gm.discard_pile.Add(card.data);
        GameManager.gm.player_hand.Remove(card.data);

        GameManager.gm.player_sweetness += card.data.Sweetness;
        GameManager.gm.ai_sweetness -= card.data.Sabotage;
        GameManager.gm.player_points -= card.data.cost;

        card_count++;
        print("Card Discarded: " + card.data.card_name + " | Total: " + card_count);
        print("Player points remaining: " + GameManager.gm.player_points);

        card.transform.SetParent(transform, false);
        card.transform.localPosition = Vector3.zero;

        GameManager.gm.AI_Turn();
        GameManager.gm.CheckWinCondition();
    }
}