using UnityEngine.EventSystems;

namespace SpellStone.Inventory
{
  public interface IInventorySlotHander
  {
    void HandleItemDrop(InventoryItemPrefab itemPrefab, PointerEventData eventData);
    bool IsSlotEmpty();
  }
}