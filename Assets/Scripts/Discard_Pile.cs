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
        card_count++;
        print("Card Discarded: " + card.data.card_name + " | Total: " + card_count);
        Destroy(card.gameObject);
    }
}