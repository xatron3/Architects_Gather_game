using UnityEngine.EventSystems;

namespace SpellStone.Inventory
{
  public class StorageChestSlot : InventorySlot
  {

    public override void HandleItemDrop(InventoryItemPrefab itemPrefab, PointerEventData eventData)
    {
      base.HandleItemDrop(itemPrefab, eventData);
    }
  }
}
