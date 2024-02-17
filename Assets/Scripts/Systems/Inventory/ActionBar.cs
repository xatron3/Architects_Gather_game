
using UnityEngine;
using System.Collections.Generic;

namespace SpellStone.Inventory
{
  public class ActionBar : MonoBehaviour
  {
    public List<ActionBarSlot> slots = new List<ActionBarSlot>();

    void Start()
    {
    }

    void Update()
    {
      // Handle keyboard input for item equipping
      for (int i = 0; i < slots.Count; i++)
      {
        if (Input.GetKeyDown(KeyCode.Alpha1 + i)) // Assuming keys 1-8
        {
          EquipItem(i);
        }
      }
    }

    void EquipItem(int slotIndex)
    {
      ActionBarSlot slot = slots[slotIndex];
      InventoryItemPrefab itemPrefab = slot.GetComponentInChildren<InventoryItemPrefab>();

      // Check if slot has an item
      if (itemPrefab != null)
      {
        // Use the item
        itemPrefab.UseItem();
      }
    }
  }
}
