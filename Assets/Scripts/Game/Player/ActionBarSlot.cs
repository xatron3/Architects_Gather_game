using UnityEngine;
using UnityEngine.EventSystems;
using SpellStone.Inventory;

public class ActionBarSlot : InventorySlot
{
  public TMPro.TextMeshProUGUI slotText;

  public void SetSlotText(string text)
  {
    slotText.text = text;
  }

  public override void AddItem(InventoryItem newItem, InventoryItemPrefab itemIconPrefab, int quantity = 1)
  {
    Debug.Log("Adding item to action bar slot. Item: " + newItem.itemName + " Quantity: " + quantity + " Can equip: " + newItem.canEquip);

    base.AddItem(newItem, itemIconPrefab, quantity);
  }

  public override void Accept(ISlotVisitor visitor)
  {
    visitor.Visit(this);
  }
}
