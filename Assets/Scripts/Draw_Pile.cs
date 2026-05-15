using UnityEngine;
using UnityEngine.EventSystems;

public class Draw_Pile : MonoBehaviour, IPointerClickHandler
{
    public int draws_remaining = 8;

    public int player_turns = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Draw pile clicked, draws remaining: " + draws_remaining);
        if (GameManager.gm.deck.Count > 0 && draws_remaining > 0)
        
        {
            Draw();
            draws_remaining--;
        }
        // else if (draws_remaining == 0)
        // {
        //     Debug.Log("No draws remaining!");
        // }
        // else
        // {
        //     Debug.Log("No cards left in deck!");
        // }
    }

    void Draw()
    {
        Card current_card = Instantiate(GameManager.gm.blank, Vector3.zero, Quaternion.identity);
        current_card.data = GameManager.gm.deck[0];
        GameManager.gm.deck.Remove(current_card.data);
        GameManager.gm.player_hand.Add(current_card.data);
        current_card.transform.SetParent(GameManager.gm.canvas.transform);
        Debug.Log("Card drawn: " + current_card.data.card_name);
    }
}
//GameManager.gm.player_deck.Count > 0 && 