using System.Collections.Generic;
using SpellStone.Inventory;

public interface IPlayerInventory
{
  bool ContainsItem(InventoryItem item);
  bool ContainsItems(List<InventoryItem> items);

  bool AddItem(InventoryItem item, int quantity = 1);

  void RemoveItem(InventoryItem item);
  void RemoveItems(List<InventoryItem> items);

  int GetTotalItemsOfName(string itemName);
  int GetTotalItems();
  int GetFreeSlots();
}