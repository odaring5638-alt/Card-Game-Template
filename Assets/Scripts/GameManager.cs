using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public List<Card_Data> deck = new List<Card_Data>();
    public List<Card_Data> player_deck = new List<Card_Data>();
    public List<Card_Data> ai_deck = new List<Card_Data>();
    public List<Card_Data> player_hand = new List<Card_Data>();
    //public List<Card_Data> ai_hand = new List<Card_Data>();
    public List<Card_Data> discard_pile = new List<Card_Data>();

    public List<Card_Data> ai_hand = new List<Card_Data>();
    public Vector3 ai_hand_spawnpoint;

    public Card blank;
    public Vector3 player_hand_spawnpoint;
    public Vector3 offset;

    public Canvas canvas;

    public int player_sweetness = 0;
public int ai_sweetness = 0;

public void AI_Turn()
{
    if (ai_hand.Count == 0)
    {
        Debug.Log("AI has no cards left!");
        CheckWinCondition();
        return;
    }

    // Pick a random card from ai_hand
    System.Random rng = new System.Random();
    int randomIndex = rng.Next(0, ai_hand.Count);
    Card_Data chosen = ai_hand[randomIndex];

    // Apply the card effects
    ai_sweetness += chosen.Sweetness;
    player_sweetness -= chosen.Sabotage;

    ai_hand.RemoveAt(randomIndex);
    discard_pile.Add(chosen);

    Debug.Log("AI played: " + chosen.card_name);
    Debug.Log("AI sweetness: " + ai_sweetness + " | Player sweetness: " + player_sweetness);
}

void CheckWinCondition()
{
    if (player_hand.Count == 0 && ai_hand.Count == 0)
    {
        if (player_sweetness > ai_sweetness)
            Debug.Log("Player wins!");
        else if (ai_sweetness > player_sweetness)
            Debug.Log("AI wins!");
        else
            Debug.Log("Its a tie!");
    }
}

    private void Awake()
    {
        if (gm != null && gm != this)
        {
            Destroy(gameObject);
        }
        else
        {
            gm = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        Deal();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Deal()
    {
        Shuffle(player_deck);
        Shuffle(ai_deck);

        Vector3 playerOffset = Vector3.zero;
        Vector3 aiOffset = Vector3.zero;

        for (int i = 0; i < 4; i++)
        {
            // Player card
            Card playerCard = Instantiate(blank, player_hand_spawnpoint + playerOffset, Quaternion.identity);
            playerCard.data = player_deck[0];
            player_deck.RemoveAt(0);
            player_hand.Add(playerCard.data);
            playerCard.transform.SetParent(canvas.transform, false);
            playerOffset.x += 100;

            // AI card
            Card aiCard = Instantiate(blank, ai_hand_spawnpoint + aiOffset, Quaternion.identity);
            aiCard.data = ai_deck[0];
            ai_deck.RemoveAt(0);
            ai_hand.Add(aiCard.data);
            aiCard.transform.SetParent(canvas.transform, false);
            aiOffset.x += 100;
        }
    }

    void Shuffle(List<Card_Data> _deck)
    {
        System.Random rngg = new System.Random();
        for (int i = 0; i < _deck.Count; i++)
        {
            int randomIndex = rngg.Next(i, _deck.Count);
            Card_Data temp = _deck[i];
            _deck[i] = _deck[randomIndex];
            _deck[randomIndex] = temp;
        }
    }

    

    void Player_Turn()
    {
        // 
    }

    
}
