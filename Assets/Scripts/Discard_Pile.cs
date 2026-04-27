using UnityEngine;

public class Discard_Pile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //delete a card when a card touches it
    void OnTriggerEnter2D(Collider2D other)
    {
        print("Card Discarded");
        if (other.CompareTag("Card"))
        {
            Destroy(other.gameObject);
        }
    }
    //keep track of cards that have been discarded
    public int card_count = 0;
    
}