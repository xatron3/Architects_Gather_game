using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SpellStone.Inventory
{
  public class InventorySlot : MonoBehaviour, IDropHandler
  {
    public void AddItem(InventoryItem newItem, InventoryItemPrefab itemIconPrefab)
    {
      InventoryItemPrefab itemPrefab = Instantiate(itemIconPrefab, transform);
      itemPrefab.SetItem(newItem);
    }

    public void ClearSlot()
    {
      if (transform.childCount > 0)
      {
        Destroy(transform.GetChild(0).gameObject);
      }
    }

    public void OnDrop(PointerEventData eventData)
    {
      // Check if there is an item already in the slot
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