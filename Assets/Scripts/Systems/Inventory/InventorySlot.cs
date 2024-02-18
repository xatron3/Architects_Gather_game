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

    public virtual void OnDrop(PointerEventData eventData)
    {
      if (transform.childCount > 0)
      {
        Debug.Log("Slot is already occupied");
        return;
      }

      GameObject droppedItem = eventData.pointerDrag;

      if (droppedItem != null)
      {
        InventoryItemPrefab InventoryItemPrefab = droppedItem.GetComponent<InventoryItemPrefab>();
        if (InventoryItemPrefab != null)
        {
          InventoryItemPrefab.parentToReturnTo = transform;
        }
      }
    }
  }
}