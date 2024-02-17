using System.Collections.Generic;
using UnityEngine;

namespace SpellStone.Inventory
{
  public class InventoryGrid : MonoBehaviour
  {
    public List<InventorySlot> slots = new List<InventorySlot>();

    public void AddItem(InventoryItem newItem)
    {
      foreach (InventorySlot slot in slots)
      {
        if (slot.GetComponentInChildren<InventoryItemPrefab>() == null)
        {
          slot.AddItem(newItem);
          return;
        }
      }
    }

    public void RemoveItem(InventoryItem item)
    {
      foreach (InventorySlot slot in slots)
      {
        if (slot.GetComponentInChildren<InventoryItemPrefab>() == item)
        {
          slot.ClearSlot();
          return;
        }
      }
    }

    public int GetTotalItems()
    {
      int totalItems = 0;

      foreach (InventorySlot slot in slots)
      {
        if (slot.GetComponentInChildren<InventoryItemPrefab>() != null)
          totalItems++;
      }

      return totalItems;
    }

    public int GetFreeSlots()
    {
      int freeSlots = 0;

      foreach (InventorySlot slot in slots)
      {
        if (slot.GetComponentInChildren<InventoryItemPrefab>() == null)
          freeSlots++;
      }

      return freeSlots;
    }
  }
}