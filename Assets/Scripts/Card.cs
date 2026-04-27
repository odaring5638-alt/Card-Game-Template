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

    private bool isDragging = false;
    private Vector3 offset;
    private Camera mainCamera;
    private Canvas canvas;
    private RectTransform rectTransform;
        

    // Start is called before the first frame update
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
    }

    // Update is called once per frame
    void Update()
    {
        // Mouse pressed — try to start dragging
        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            TryStartDrag();
        }
        // Mouse released — stop dragging
        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            isDragging = false;
        }
        // Every frame while dragging — follow the mouse
        if (isDragging)
        {
            DragObject();
        }
    }

    void TryStartDrag()
    {
        // Same raycast logic as the click script
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(
        new Vector3(mousePos.x, mousePos.y, 0));
        RaycastHit2D hit = Physics2D.Raycast(worldPos, Vector2.zero);
        if (hit.collider != null && hit.collider.gameObject == gameObject)
        {
            isDragging = true;
            // Remember where on the sprite the mouse grabbed it
            offset = transform.position - worldPos;
            offset.z = 0;
        }
    }
    
    void DragObject()
    {
        Vector2 mousePos = Mouse.current.position.ReadValue();
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(
        new Vector3(mousePos.x, mousePos.y, 0));
        worldPos.z = transform.position.z;
        // Apply the offset so the sprite doesn't snap to cursor center
        transform.position = worldPos + offset;
    }

    
    private Vector3 mousePositionOffset;

    private Vector3 GetMouseWorldPosition() {
    // Captures mouse position and converts it to World Space
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void OnMouseDown() {
    // Calculate the difference between the object's origin and the click point
        mousePositionOffset = gameObject.transform.position - GetMouseWorldPosition();
    }

    private void OnMouseDrag() {
    // Continuously update position while the mouse is held down
        transform.position = GetMouseWorldPosition() + mousePositionOffset; 
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        // Called once when the drag starts
        Debug.Log("Started dragging " + gameObject.name);
    }
    public void OnDrag(PointerEventData eventData)
    {
        // Called every frame while dragging
        rectTransform.anchoredPosition += eventData.delta /
        canvas.scaleFactor;
    }
    public void OnEndDrag(PointerEventData eventData)
    {
        // Called once when the drag ends
        Debug.Log("Finished dragging " + gameObject.name);
    }
}
