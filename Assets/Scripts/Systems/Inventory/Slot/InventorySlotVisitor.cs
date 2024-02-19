using UnityEngine;
using SpellStone.Inventory;

public class InventorySlotVisitor : ISlotVisitor
{
  private InventoryItemPrefab _itemPrefab;
  public bool DropSuccessful { get; private set; }

  public InventorySlotVisitor(InventoryItemPrefab itemPrefab)
  {
    _itemPrefab = itemPrefab;
  }

  public void Visit(InventorySlot slot)
  {
    // Handle InventorySlot logic here
    // For example:
    slot.parentGrid.items.Add(_itemPrefab.GetItem());
    _itemPrefab.parentToReturnTo.GetComponent<InventorySlot>().parentGrid.items.Remove(_itemPrefab.GetItem());
    _itemPrefab.GetItem().SetSlotIndex(slot.GetSlotIndex());

    Debug.Log("Dropping item to: " + slot.name);
    _itemPrefab.parentToReturnTo = slot.transform;
    _itemPrefab.transform.SetParent(slot.transform); // Set the parent of the dropped item to the new slot
    _itemPrefab.transform.localPosition = Vector3.zero; // Reset local position

    DropSuccessful = true;
  }

  public void Visit(ActionBarSlot slot)
  {
    // Handle ActionBarSlot logic here
    if (!_itemPrefab.GetItem().canEquip)
    {
      Debug.Log("Cannot equip this item to ActionBar because canEquip is false.");
      DropSuccessful = false;
      return;
    }

    slot.parentGrid.items.Add(_itemPrefab.GetItem());
    _itemPrefab.parentToReturnTo.GetComponent<InventorySlot>().parentGrid.items.Remove(_itemPrefab.GetItem());
    _itemPrefab.GetItem().SetSlotIndex(slot.GetSlotIndex());

    Debug.Log("Dropping item to ActionBar: " + slot.name);
    _itemPrefab.parentToReturnTo = slot.transform;
    _itemPrefab.transform.SetParent(slot.transform); // Set the parent of the dropped item to the new slot
    _itemPrefab.transform.localPosition = Vector3.zero; // Reset local position

    DropSuccessful = true;
  }
}
