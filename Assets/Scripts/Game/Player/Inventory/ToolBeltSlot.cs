using UnityEngine;
using UnityEngine.UI;
using SpellStone.Inventory;

public class ToolBeltSlot : InventorySlot
{
  public override void Accept(ISlotVisitor visitor)
  {
    visitor.Visit(this);
  }
}
