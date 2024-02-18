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
  }
}
