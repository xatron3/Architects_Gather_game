using UnityEngine;
using System.Collections.Generic;
using SpellStone.Inventory;

namespace SpellStone.ActionBar
{
  public class ActionBarGrid : MonoBehaviour
  {
    public List<ActionBarSlot> slots = new List<ActionBarSlot>();

    public void AddItem(InventoryItem newItem, InventoryItemPrefab itemIconPrefab)
    {
      foreach (ActionBarSlot slot in slots)
      {
        if (slot.GetComponentInChildren<InventoryItemPrefab>() == null)
        {
          slot.AddItem(newItem, itemIconPrefab);
          return;
        }
      }
    }
  }
}
