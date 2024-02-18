using SpellStone.Inventory;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class StorageChestSlot : InventorySlot
{
  public event Action<InventoryItem> OnItemDropped;

  public override void OnDrop(PointerEventData eventData)
  {
    base.OnDrop(eventData);

    GameObject droppedItem = eventData.pointerDrag;

    if (droppedItem != null)
    {
      InventoryItemPrefab inventoryItemPrefab = droppedItem.GetComponent<InventoryItemPrefab>();
      OnItemDropped?.Invoke(inventoryItemPrefab.GetItem());
    }
  }
}
