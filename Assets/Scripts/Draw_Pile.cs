using UnityEngine;

public class Draw_Pile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //spawn in a card when clicked
    void Draw()
    {
        //instantiate a card from the player deck and add it to the player hand
        Card current_card = Instantiate(GameManager.gm.blank, transform.position, Quaternion.identity);
        current_card.data = GameManager.gm.player_deck[0];
        GameManager.gm.player_deck.Remove(current_card.data);
        GameManager.gm.player_hand.Add(current_card.data);
        current_card.transform.SetParent(GameManager.gm.canvas.transform);
    }

    void OnMouseDown()
    {
        if (GameManager.gm.player_deck.Count > 0)
        {
            Draw();
        }
    }

}
