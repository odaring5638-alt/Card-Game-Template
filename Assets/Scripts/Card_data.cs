using UnityEngine;

[CreateAssetMenu(fileName = "Card_Data", menuName = "Scriptable Objects/Card_Data")]
public class Card_Data : ScriptableObject
{
    public string card_name;
    public string description;
    public int Sweetness;
    public int cost;
    public int Sabotage;
    public Sprite sprite;   

    

}
