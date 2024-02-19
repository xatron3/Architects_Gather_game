using System;
using UnityEngine;

namespace SpellStone.Inventory
{
  public class InventoryManager
  {
    public bool AddItem(InventoryGrid inventoryGrid, InventoryItem newItem, InventoryItemPrefab itemIconPrefab, int quantity = 1, bool lookForUnique = false, int slotIndex = -1)
    {
      // Add the item instance to the inventory grid
      return inventoryGrid.AddItem(newItem, itemIconPrefab, quantity, lookForUnique, slotIndex);
    }

    public void RemoveItem(InventoryGrid inventoryGrid, InventoryItem item)
    {
      // Remove the item instance from the inventory grid
      inventoryGrid.RemoveItem(item);
    }
  }
}
