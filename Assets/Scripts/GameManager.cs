using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager gm;
    public List<Card_Data> deck = new List<Card_Data>();
    public List<Card_Data> player_deck = new List<Card_Data>();
    public List<Card_Data> ai_deck = new List<Card_Data>();
    public List<Card_Data> player_hand = new List<Card_Data>();
    public List<Card_Data> ai_hand = new List<Card_Data>();
    public List<Card_Data> discard_pile = new List<Card_Data>();

    public List<Card> ai_card_objects = new List<Card>();

    public int player_sweetness = 0;
    public int ai_sweetness = 0;
    public int player_points = 35;
    public int ai_points = 35;

    public Card blank;
    public Vector3 player_hand_spawnpoint;
    public Vector3 ai_hand_spawnpoint;
    public Vector3 offset;

    public Canvas canvas;
    public Transform discard_pile_transform;

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

    void Start()
    {
        Deal();
    }

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
            playerCard.owner = Card.Owner.Player;
            player_deck.RemoveAt(0);
            player_hand.Add(playerCard.data);
            playerCard.transform.SetParent(canvas.transform, false);
            playerCard.transform.localPosition = player_hand_spawnpoint + playerOffset;
            playerOffset.x += 100;

            // AI card
            Card aiCard = Instantiate(blank, ai_hand_spawnpoint + aiOffset, Quaternion.identity);
            aiCard.data = ai_deck[0];
            aiCard.owner = Card.Owner.AI;
            ai_deck.RemoveAt(0);
            ai_hand.Add(aiCard.data);
            aiCard.transform.SetParent(canvas.transform, false);
            aiCard.transform.localPosition = ai_hand_spawnpoint + aiOffset;
            aiOffset.x += 100;
            ai_card_objects.Add(aiCard);
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

    public void AI_Turn()
    {
        if (ai_card_objects.Count == 0)
        {
            Debug.Log("AI has no cards left!");
            CheckWinCondition();
            return;
        }

        List<Card> affordable = ai_card_objects.FindAll(c => c.data.cost <= ai_points);

        if (affordable.Count == 0)
        {
            Debug.Log("AI cant afford any cards!");
            CheckWinCondition();
            return;
        }

        System.Random rng = new System.Random();
        int randomIndex = rng.Next(0, affordable.Count);
        Card chosenCard = affordable[randomIndex];

        ai_sweetness += chosenCard.data.Sweetness;
        player_sweetness -= chosenCard.data.Sabotage;
        ai_points -= chosenCard.data.cost;

        ai_hand.Remove(chosenCard.data);
        discard_pile.Add(chosenCard.data);
        ai_card_objects.Remove(chosenCard);

        chosenCard.transform.SetParent(discard_pile_transform, false);
        chosenCard.transform.localPosition = Vector3.zero;

        Debug.Log("AI played: " + chosenCard.data.card_name);
        Debug.Log("AI points remaining: " + ai_points);
        Debug.Log("AI sweetness: " + ai_sweetness + " | Player sweetness: " + player_sweetness);

        CheckWinCondition();
    }

    public void CheckWinCondition()
    {
        if (player_points <= 0 || ai_points <= 0)
        {
            if (player_sweetness > ai_sweetness)
                Debug.Log("Player wins!");
            else if (ai_sweetness > player_sweetness)
                Debug.Log("AI wins!");
            else
                Debug.Log("Its a tie!");
        }
    }

    void Player_Turn()
    {

    }
}