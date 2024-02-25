using UnityEngine;
using SpellStone.Inventory;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/New Tool Item")]
public class ToolItem : InventoryItem
{
  [Header("Tool Item")]
  public ToolType toolType;

  public override void Use(PlayerController player)
  {
    base.Use(player);
    Debug.Log("Using " + itemName);
  }

  public override void Equip()
  {
    Debug.Log("Equipping " + itemName);
  }

  public override void Unequip()
  {
    Debug.Log("Unequipping " + itemName);
  }
}

public enum ToolType
{
  Axe,
  Pickaxe,
}