using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SpellStone.Inventory
{
  public class InventorySlot : MonoBehaviour, IDropHandler
  {
    public void AddItem(InventoryItem newItem, InventoryItemPrefab itemIconPrefab, int quantity = 1)
    {
      // Check if the item is stackable and if it's already in the slot
      if (newItem.isStackable && !IsSlotEmpty() && transform.GetChild(0).GetComponent<InventoryItemPrefab>().GetItem().itemName == newItem.itemName)
      {
        transform.GetChild(0).GetComponent<InventoryItemPrefab>().GetItem().currentStackSize += quantity;
        transform.GetChild(0).GetComponent<InventoryItemPrefab>().UpdateStackSize();
        return;
      }

      InventoryItemPrefab itemPrefab = Instantiate(itemIconPrefab, transform);
      itemPrefab.SetItem(newItem);
    }

    public void ClearSlot()
    {
      if (!IsSlotEmpty())
      {
        DestroyImmediate(transform.GetChild(0).gameObject);
      }
    }

    public bool IsSlotEmpty()
    {
      return transform.childCount == 0;
    }

    public void OnDrop(PointerEventData eventData)
    {
      if (!DropItem(eventData))
      {
        Debug.Log("Slot is already occupied or drop failed");
        return;
      }

      // Call a protected method to allow child classes to perform additional logic
      OnDropSuccess(eventData);
    }

    // Protected method that can be overridden by child classes to perform additional logic
    protected virtual void OnDropSuccess(PointerEventData eventData)
    {
      // Default implementation does nothing
    }

    // Internal method to handle dropping an item
    private bool DropItem(PointerEventData eventData)
    {
      if (transform.childCount > 0)
      {
        return false; // Drop failed if the slot is already occupied
      }

      GameObject droppedItem = eventData.pointerDrag;

      if (droppedItem != null)
      {
        InventoryItemPrefab inventoryItemPrefab = droppedItem.GetComponent<InventoryItemPrefab>();
        if (inventoryItemPrefab != null)
        {
          inventoryItemPrefab.parentToReturnTo = transform;
          return true; // Drop successful
        }
      }

      return false; // Drop failed if no item was dropped
    }
  }
}