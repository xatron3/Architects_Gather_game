using System;
using UnityEngine;

namespace SpellStone.Inventory
{
  public class InventoryManager
  {
    public bool AddItem(InventoryGrid inventoryGrid, InventoryItem newItem, InventoryItemPrefab itemIconPrefab)
    {
      // Add the item instance to the inventory grid
      return inventoryGrid.AddItem(newItem, itemIconPrefab);
    }

    public void RemoveItem(InventoryGrid inventoryGrid, InventoryItem item)
    {
      // Remove the item instance from the inventory grid
      inventoryGrid.RemoveItem(item);
    }
  }
}
