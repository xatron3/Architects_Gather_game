using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

namespace SpellStone.Inventory
{
  public class InventoryItemPrefab : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, ITooltip, IPointerEnterHandler, IPointerExitHandler
  {
    [HideInInspector] public Transform parentToReturnTo = null;
    public Image icon { get; private set; }
    public TMP_Text stackSizeText { get; private set; }
    public InventoryItem item { get; private set; }

    private InventorySlot currentSlot;

    public void UseItem(PlayerController player)
    {
      if (item != null)
      {
        item.Use(player);
      }
    }

    public void SetIconSprite(Sprite sprite)
    {
      if (icon == null)
        icon = GetComponent<Image>();

      icon.sprite = sprite;
    }

    private void SetStackSizeText(int stackSize)
    {
      if (stackSizeText == null)
        stackSizeText = GetComponentInChildren<TMP_Text>();

      if (item.isStackable == false)
      {
        stackSizeText.gameObject.SetActive(false);
        return;
      }

      stackSizeText.text = $"{stackSize}";
    }

    public InventoryItem GetItem()
    {
      if (item != null)
      {
        return item;
      }
      else
      {
        return null;
      }
    }

    public void SetItem(InventoryItem newItem)
    {
      item = newItem;
      SetIconSprite(item.icon);
      SetStackSizeText(item.currentStackSize);
    }

    public void UpdateStackSize()
    {
      SetStackSizeText(item.currentStackSize);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
      currentSlot = GetComponentInParent<InventorySlot>();

      parentToReturnTo = transform.parent;

      if (transform.root.GetComponent<Canvas>() != null)
        transform.SetParent(transform.root.GetComponent<Canvas>().transform, true);
      else if (transform.root.GetComponentInChildren<Canvas>() != null)
        transform.SetParent(transform.root.GetComponentInChildren<Canvas>().transform, true);
      else
        transform.SetParent(transform.parent.parent, true);

      transform.SetAsLastSibling();
      icon.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
      transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      currentSlot?.HandleItemDrop(this, eventData);
      transform.SetParent(parentToReturnTo, true);
      icon.raycastTarget = true;
    }

    public string GetTooltipContent()
    {
      if (item != null)
      {
        return item.GetTooltipContent();
      }
      else
      {
        return "Unknown item";
      }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
      if (item != null)
      {
        // Get the width of the tooltip panel and the width of the game object that we are hovering over
        float tooltipPanelWidth = TooltipManager.Instance.tooltipPanel.GetComponent<RectTransform>().rect.width;
        float itemWidth = GetComponent<RectTransform>().rect.width;

        // Calculate the position of the tooltip panel
        Vector2 position = new Vector2(transform.position.x + (itemWidth / 2) + (tooltipPanelWidth / 2), transform.position.y);

        TooltipManager.Instance.ShowTooltip(GetTooltipContent(), position);
      }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
      TooltipManager.Instance.HideTooltip();
    }
  }
}