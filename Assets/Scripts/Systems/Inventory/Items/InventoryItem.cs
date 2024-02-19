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

    public virtual void Use()
    {
      Debug.Log("Using " + itemName);
    }

    public InventoryItem GetCopy()
    {
      return Instantiate(this);
    }
  }
}
