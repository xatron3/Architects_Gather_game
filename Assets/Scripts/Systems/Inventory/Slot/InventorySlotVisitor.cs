using UnityEngine;
using SpellStone.Inventory;
using SpellStone.Messages;

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

  public void Visit(ToolBeltSlot slot)
  {
    // Check if item is an tool item
    if (_itemPrefab.GetItem() is not ToolItem)
    {
      DropSuccessful = false;
      return;
    }

    // Check if the player already has an tool of this type in the tool belt
    foreach (var item in slot.parentGrid.items)
    {
      if (item is ToolItem toolItem && toolItem.toolType == (_itemPrefab.GetItem() as ToolItem).toolType)
      {
        MessagingService.Instance.ShowMessage("You already have an tool of this type in your tool belt.", Color.red);
        DropSuccessful = false;
        return;
      }
    }

    slot.parentGrid.items.Add(_itemPrefab.GetItem());
    _itemPrefab.parentToReturnTo.GetComponent<InventorySlot>().parentGrid.items.Remove(_itemPrefab.GetItem());
    _itemPrefab.GetItem().SetSlotIndex(slot.GetSlotIndex());

    _itemPrefab.parentToReturnTo = slot.transform;
    _itemPrefab.transform.SetParent(slot.transform);
    _itemPrefab.transform.localPosition = Vector3.zero;

    DropSuccessful = true;
  }
}
