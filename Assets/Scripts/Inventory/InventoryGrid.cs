using System.Collections.Generic;
using UnityEngine;

public class InventoryGrid : MonoBehaviour
{
  public List<ItemSlot> slots = new List<ItemSlot>();

  public void AddItem(Item newItem)
  {
    foreach (ItemSlot slot in slots)
    {
      if (slot.item == null)
      {
        slot.AddItem(newItem);
        return;
      }
    }
  }

  public void RemoveItem(Item item)
  {
    foreach (ItemSlot slot in slots)
    {
      if (slot.item == item)
      {
        slot.ClearSlot();
        return;
      }
    }
  }

  public int GetTotalItems()
  {
    int totalItems = 0;

    foreach (ItemSlot slot in slots)
    {
      if (slot.item != null)
        totalItems++;
    }

    return totalItems;
  }

  public int GetFreeSlots()
  {
    int freeSlots = 0;

    foreach (ItemSlot slot in slots)
    {
      if (slot.item == null)
        freeSlots++;
    }

    return freeSlots;
  }
}