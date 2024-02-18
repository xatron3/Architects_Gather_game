using System;
using UnityEngine;

namespace SpellStone.Inventory
{
  [CreateAssetMenu(fileName = "New Item", menuName = "Items/New Item")]
  public class InventoryItem : ScriptableObject
  {
    public string itemName = "New Item";
    public Sprite icon = null;
    public bool canEquip = false;

    [Header("Stackable")]
    public bool isStackable = false;
    public int maxStackSize = 1;

    public virtual void Use()
    {
      Debug.Log("Using " + itemName);
    }
  }
}