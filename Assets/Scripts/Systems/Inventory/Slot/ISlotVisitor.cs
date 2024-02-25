using SpellStone.Inventory;

public interface ISlotVisitor
{
  void Visit(InventorySlot slot);
  void Visit(ActionBarSlot slot);
  void Visit(ToolBeltSlot slot);
}
