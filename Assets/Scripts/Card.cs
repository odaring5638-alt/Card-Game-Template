using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class Card : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Card_Data data;

    public string card_name;
    public string description;
    public int Sweetness;
    public int cost;
    public int Sabotage;
    public Sprite sprite;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI healthText;
    public TextMeshProUGUI costText;
    public TextMeshProUGUI damageText;
    public Image spriteImage;

    private Camera mainCamera;
    private Canvas canvas;
    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;

    void Start()
    {
        card_name = data.card_name;
        description = data.description;
        Sweetness = data.Sweetness;
        cost = data.cost;
        Sabotage = data.Sabotage;
        sprite = data.sprite;
        nameText.text = card_name;
        descriptionText.text = description;
        healthText.text = Sweetness.ToString();
        costText.text = cost.ToString();
        damageText.text = Sabotage.ToString();
        spriteImage.sprite = sprite;
        mainCamera = Camera.main;
        canvas = FindAnyObjectByType<Canvas>();
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        rectTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        Debug.Log("Drop detected, hovering over " + eventData.hovered.Count + " objects");
        foreach (GameObject obj in eventData.hovered)
        {
            Debug.Log("Hovered: " + obj.name);
            Discard_Pile pile = obj.GetComponent<Discard_Pile>();
            if (pile != null)
            {
                pile.DiscardCard(this);
                return;
            }
        }
    }
}
