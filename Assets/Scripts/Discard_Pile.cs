using System.Collections.Generic;
using UnityEngine;

public class Discard_Pile : MonoBehaviour
{
    public int card_count = 0;
    public List<Card_Data> discarded_cards = new List<Card_Data>();

    public void DiscardCard(Card card)
    {
        discarded_cards.Add(card.data);
        GameManager.gm.discard_pile.Add(card.data);
        GameManager.gm.player_hand.Remove(card.data);

        GameManager.gm.player_sweetness += card.data.Sweetness;
        GameManager.gm.ai_sweetness -= card.data.Sabotage;

        card_count++;
        print("Card Discarded: " + card.data.card_name + " | Total: " + card_count);

        card.transform.SetParent(transform, false);
        card.transform.localPosition = Vector3.zero;

        GameManager.gm.AI_Turn();
    }
}