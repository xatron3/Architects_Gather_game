using System.Collections.Generic;
using UnityEngine;

namespace SpellStone.Inventory
{
  public class InventoryGrid : MonoBehaviour
  {
    public List<InventorySlot> slots = new List<InventorySlot>();

    public void AddItem(InventoryItem newItem, InventoryItemPrefab itemIconPrefab)
    {
      foreach (InventorySlot slot in slots)
      {
        if (slot.GetComponentInChildren<InventoryItemPrefab>() == null)
        {
          slot.AddItem(newItem, itemIconPrefab);
          return;
        }
      }
    }

    public void RemoveItem(InventoryItem item)
    {
      Debug.Log("Removing item: " + item.itemName + " from inventory grid");
      foreach (InventorySlot slot in slots)
      {
        if (slot.GetComponentInChildren<InventoryItemPrefab>().item.itemName == item.itemName)
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

    public int GetTotalItemsOfName(string itemName)
    {
      int totalItems = 0;

      foreach (InventorySlot slot in slots)
      {
        InventoryItemPrefab itemPrefab = slot.GetComponentInChildren<InventoryItemPrefab>();

        if (itemPrefab != null && itemPrefab.GetItem().itemName == itemName)
          totalItems++;
      }

      return totalItems;
    }
  }
}