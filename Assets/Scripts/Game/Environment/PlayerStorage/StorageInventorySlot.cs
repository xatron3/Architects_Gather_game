using SpellStone.Inventory;
using UnityEngine.EventSystems;
using UnityEngine;

public class StorageInventorySlot : InventorySlot
{
  public override void OnDrop(PointerEventData eventData)
  {
    base.OnDrop(eventData);

    GameObject droppedItem = eventData.pointerDrag;

    if (droppedItem != null)
    {
      InventoryItemPrefab InventoryItemPrefab = droppedItem.GetComponent<InventoryItemPrefab>();
      EnvPlayerStorage.playerStorageItems.Add(InventoryItemPrefab.GetItem());
    }
  }
}
