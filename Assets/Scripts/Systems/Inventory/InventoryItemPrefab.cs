using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

namespace SpellStone.Inventory
{
  public class InventoryItemPrefab : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
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
      transform.SetParent(transform.parent.parent);
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
  }
}