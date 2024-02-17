using UnityEngine;
using UnityEngine.EventSystems;
using SpellStone.Inventory;

namespace SpellStone.ActionBar
{
  public class ActionBarSlot : MonoBehaviour, IDropHandler
  {
    public void OnDrop(PointerEventData eventData)
    {
      if (transform.childCount > 0)
      {
        Debug.Log("Slot is not empty");
        return;
      }

      GameObject droppedItem = eventData.pointerDrag;

      if (droppedItem != null)
      {
        InventoryItemPrefab inventoryItemPrefab = droppedItem.GetComponent<InventoryItemPrefab>();
        if (inventoryItemPrefab != null)
        {
          InventoryItem item = inventoryItemPrefab.GetItem();

          if (item.canEquip)
            inventoryItemPrefab.parentToReturnTo = transform;
        }
      }
    }

    public void AddItem(InventoryItem newItem, InventoryItemPrefab itemIconPrefab)
    {
      InventoryItemPrefab itemPrefab = Instantiate(itemIconPrefab, transform);
      itemPrefab.SetItem(newItem);
    }
  }
}
