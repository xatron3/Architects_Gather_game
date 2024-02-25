using System;
using UnityEngine;

namespace SpellStone.Inventory
{
  [CreateAssetMenu(fileName = "New Item", menuName = "Items/New Item")]
  public class InventoryItem : ScriptableObject
  {
    [HideInInspector] public Guid uniqueID;
    public int slotIndex = -1;
    public string itemName = "New Item";
    public Sprite icon = null;
    public bool canEquip = false;

    [Header("Stackable")]
    public bool isStackable = false;
    public int maxStackSize = 1;
    public int currentStackSize = 1;

    [Header("Others")]
    public bool breakOnUse = false;

    private void OnEnable()
    {
      uniqueID = Guid.NewGuid();
    }

    public Guid GetUniqueID()
    {
      return uniqueID;
    }

    public void SetSlotIndex(int index)
    {
      slotIndex = index;
    }

    public int GetSlotIndex()
    {
      return slotIndex;
    }

    public virtual void Equip()
    {
      Debug.Log("Equipping " + itemName);
    }

    public virtual void Unequip()
    {
      Debug.Log("Unequipping " + itemName);
    }

    public virtual void Use(PlayerController player)
    {
      Debug.Log("Using " + itemName);
    }

    public virtual InventoryItem GetCopy()
    {
      return Instantiate(this);
    }

    public virtual string GetTooltipContent()
    {
      string stackSize = isStackable ? $"x{currentStackSize}/{maxStackSize}" : null;

      return $"{itemName} {stackSize} \n";
    }
  }
}
