using UnityEngine;
using UnityEngine.EventSystems;
using System;


namespace SpellStone.Inventory
{
  public class StorageChestSlot : InventorySlot
  {
    public event Action<InventoryItem> OnItemDropped;

    public override void HandleItemDrop(InventoryItemPrefab itemPrefab, PointerEventData eventData)
    {
      base.HandleItemDrop(itemPrefab, eventData);
      // if (!IsSlotEmpty())
      // {
      //   Debug.Log("Slot is not empty");
      //   return;
      // }

      // if (itemPrefab != null)
      // {
      //   InventoryItem item = itemPrefab.GetItem();
      //   OnItemDropped?.Invoke(item);
      // }
    }
  }
}
