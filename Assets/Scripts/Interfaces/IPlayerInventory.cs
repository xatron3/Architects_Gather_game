using System.Collections.Generic;
using SpellStone.Inventory;

public interface IPlayerInventory
{
  bool HasIngredients(List<InventoryItem> items);
  void RemoveIngredients(List<InventoryItem> items);
  void AddItem(InventoryItem item);
}