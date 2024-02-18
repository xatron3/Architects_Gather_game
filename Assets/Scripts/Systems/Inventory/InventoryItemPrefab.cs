using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using System;

namespace SpellStone.Inventory
{
  public class InventoryItemPrefab : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
  {
    public event Action<InventoryItem> OnItemDroppedToInventory;

    [HideInInspector] public Transform parentToReturnTo = null;
    public Image icon { get; private set; }
    public TMP_Text stackSizeText { get; private set; }
    public InventoryItem item { get; private set; }

    private InventorySlot currentSlot;

    public void UseItem()
    {
      if (item != null)
      {
        item.Use();
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
      // SetStackSizeText(StackSize);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
      // check if it is a storage slot or inventory slot
      if (transform.parent.GetComponent<InventorySlot>() != null)
        currentSlot = transform.parent.GetComponent<InventorySlot>();
      else if (transform.parent.GetComponent<StorageChestSlot>() != null)
        currentSlot = transform.parent.GetComponent<StorageChestSlot>();
      else
        Debug.LogError("The parent of the item is not a storage or inventory slot");

      parentToReturnTo = transform.parent;
      transform.SetParent(transform.parent.parent);
      transform.SetAsLastSibling();
      icon.raycastTarget = false;
      Debug.Log("OnBeginDrag");
    }

    public void OnDrag(PointerEventData eventData)
    {
      transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      if (currentSlot != null && currentSlot.GetType() == typeof(StorageChestSlot) && eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<InventorySlot>() != null)
      {
        InventorySlot slot = eventData.pointerCurrentRaycast.gameObject.GetComponent<InventorySlot>();
        if (slot != null)
        {
          if (item != null)
          {
            OnItemDroppedToInventory?.Invoke(item);
          }
        }
      }

      transform.SetParent(parentToReturnTo, true);
      icon.raycastTarget = true;
    }
  }
}