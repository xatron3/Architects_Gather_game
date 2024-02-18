using SpellStone.Inventory;
using UnityEngine.EventSystems;
using UnityEngine;
using System;

public class StorageChestSlot : InventorySlot
{
  public event Action<InventoryItem> OnItemDropped;

  protected override void OnDropSuccess(PointerEventData eventData)
  {
    GameObject droppedItem = eventData.pointerDrag;
    if (droppedItem != null)
    {
      InventoryItemPrefab inventoryItemPrefab = droppedItem.GetComponent<InventoryItemPrefab>();
      OnItemDropped?.Invoke(inventoryItemPrefab.GetItem());
    }
  }
}
