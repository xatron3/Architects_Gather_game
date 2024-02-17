using System.Collections.Generic;
using SpellStone.Inventory;

public interface IPlayerInventory
{
  bool ContainsItem(InventoryItem item);
  bool ContainsItems(List<InventoryItem> items);

  void AddItem(InventoryItem item);

  void RemoveItem(InventoryItem item);
  void RemoveItems(List<InventoryItem> items);
}