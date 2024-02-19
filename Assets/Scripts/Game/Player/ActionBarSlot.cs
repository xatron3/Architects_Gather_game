using UnityEngine;
using UnityEngine.EventSystems;
using SpellStone.Inventory;

namespace SpellStone.ActionBar
{
  public class ActionBarSlot : InventorySlot
  {
    public TMPro.TextMeshProUGUI slotText;

    public void SetSlotText(string text)
    {
      slotText.text = text;
    }

    public override void HandleItemDrop(InventoryItemPrefab itemPrefab, PointerEventData eventData)
    {
      Debug.Log("Handling item drop in action bar slot");

      if (!CanDropItem(eventData))
      {
        return;
      }

      if (DropItem(itemPrefab, eventData))
      {
        Debug.Log("Item dropped successfully");
      }
    }

    public override bool DropItem(InventoryItemPrefab itemPrefab, PointerEventData eventData)
    {
      Debug.Log("Dropping item from action bar slot");
      return true;
    }
  }
}
