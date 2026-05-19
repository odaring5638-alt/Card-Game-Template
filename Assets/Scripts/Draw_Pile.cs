using UnityEngine;
using UnityEngine.EventSystems;

public class Draw_Pile : MonoBehaviour, IPointerClickHandler
{
    public int draws_remaining = 8;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Draw pile clicked, draws remaining: " + draws_remaining);
        if (GameManager.gm.deck.Count > 0 && draws_remaining > 0)
        {
            Draw();
            draws_remaining--;
        }
        else
        {
            Debug.Log("No cards left to draw!");
        }
    }

    void Draw()
    {
        // Player draws
        Card current_card = Instantiate(GameManager.gm.blank, Vector3.zero, Quaternion.identity);
        current_card.data = GameManager.gm.deck[0];
        current_card.owner = Card.Owner.Player;
        GameManager.gm.deck.Remove(current_card.data);
        GameManager.gm.player_hand.Add(current_card.data);
        current_card.transform.SetParent(GameManager.gm.canvas.transform, false);
        current_card.transform.localPosition = new Vector3(0, -300, 0);
        Debug.Log("Player drew: " + current_card.data.card_name);

        // AI draws
        if (GameManager.gm.deck.Count > 0)
        {
            Card aiCard = Instantiate(GameManager.gm.blank, Vector3.zero, Quaternion.identity);
            aiCard.data = GameManager.gm.deck[0];
            aiCard.owner = Card.Owner.AI;
            GameManager.gm.deck.Remove(aiCard.data);
            GameManager.gm.ai_hand.Add(aiCard.data);
            aiCard.transform.SetParent(GameManager.gm.canvas.transform, false);
            aiCard.transform.localPosition = GameManager.gm.ai_hand_spawnpoint;
            GameManager.gm.ai_card_objects.Add(aiCard);
            Debug.Log("AI drew: " + aiCard.data.card_name);
        }
    }
}