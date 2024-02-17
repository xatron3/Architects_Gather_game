using UnityEngine;
using UnityEngine.EventSystems;
using SpellStone.Inventory;

namespace SpellStone.ActionBar
{
  public class ActionBarSlot : MonoBehaviour, IDropHandler
  {
    public void OnDrop(PointerEventData eventData)
    {
      GameObject droppedItem = eventData.pointerDrag;

      if (droppedItem != null)
      {
        InventoryItemPrefab inventoryItemPrefab = droppedItem.GetComponent<InventoryItemPrefab>();
        if (inventoryItemPrefab != null)
        {
          inventoryItemPrefab.parentToReturnTo = transform;
        }
      }
    }
  }
}
