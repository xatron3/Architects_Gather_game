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
    slot.parentGrid.items.Add(_itemPrefab.GetItem());
    _itemPrefab.parentToReturnTo.GetComponent<InventorySlot>().parentGrid.items.Remove(_itemPrefab.GetItem());
    _itemPrefab.GetItem().SetSlotIndex(slot.GetSlotIndex());

    Debug.Log("Dropping item to: " + slot.name);
    _itemPrefab.parentToReturnTo = slot.transform;
    _itemPrefab.transform.SetParent(slot.transform);
    _itemPrefab.transform.localPosition = Vector3.zero;

    DropSuccessful = true;
  }

  public void Visit(ActionBarSlot slot)
  {
    if (!_itemPrefab.GetItem().canEquip)
    {
      DropSuccessful = false;
      return;
    }

    slot.parentGrid.items.Add(_itemPrefab.GetItem());
    _itemPrefab.parentToReturnTo.GetComponent<InventorySlot>().parentGrid.items.Remove(_itemPrefab.GetItem());
    _itemPrefab.GetItem().SetSlotIndex(slot.GetSlotIndex());

    _itemPrefab.parentToReturnTo = slot.transform;
    _itemPrefab.transform.SetParent(slot.transform);
    _itemPrefab.transform.SetAsFirstSibling();
    _itemPrefab.transform.localPosition = Vector3.zero;

    DropSuccessful = true;
  }
}
