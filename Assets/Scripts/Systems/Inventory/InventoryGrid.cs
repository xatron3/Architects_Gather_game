using System.Collections.Generic;
using UnityEngine;

namespace SpellStone.Inventory
{
  public class InventoryGrid : MonoBehaviour
  {
    public List<InventorySlot> slots = new List<InventorySlot>();
    public List<InventoryItem> items = new List<InventoryItem>();

    public void InitializeGrid(int slotCount, InventorySlot inventorySlotPrefab, Transform parent)
    {
      for (int i = 0; i < slotCount; i++)
      {
        InventorySlot slot = Instantiate(inventorySlotPrefab, parent, false);
        slots.Add(slot);
      }
    }

    public bool AddItem(InventoryItem newItem, InventoryItemPrefab itemIconPrefab, int quantity = 1)
    {
      foreach (InventorySlot slot in slots)
      {
        bool IsSlotEmpty = slot.IsSlotEmpty();
        bool isStackable = false;
        bool isMaxStackSize = true;

        if (slot.GetComponentInChildren<InventoryItemPrefab>() != null)
        {
          InventoryItemPrefab _inventoryItemPrefab = slot.GetComponentInChildren<InventoryItemPrefab>();
          InventoryItem _inventoryItem = _inventoryItemPrefab.GetItem();

          isStackable = _inventoryItem.itemName == newItem.itemName;
          isMaxStackSize = _inventoryItem.currentStackSize + quantity > _inventoryItem.maxStackSize;
        }

        if (IsSlotEmpty || isStackable && !isMaxStackSize)
        {
          slot.AddItem(newItem, itemIconPrefab);
          items.Add(newItem);
          return true;
        }
      }

      return false;
    }

    public void RemoveItem(InventoryItem item, int quantity = 1)
    {
      foreach (InventorySlot slot in slots)
      {
        if (slot.GetComponentInChildren<InventoryItemPrefab>() != null)
        {
          InventoryItemPrefab _inventoryItemPrefab = slot.GetComponentInChildren<InventoryItemPrefab>();
          InventoryItem _inventoryItem = _inventoryItemPrefab.GetItem();

          if (_inventoryItem.itemName == item.itemName)
          {
            if (_inventoryItem.currentStackSize > quantity)
            {
              _inventoryItem.currentStackSize -= quantity;
              _inventoryItemPrefab.UpdateStackSize();
              return;
            }
            else
            {
              slot.ClearSlot();
              items.Remove(item);
              return;
            }
          }
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