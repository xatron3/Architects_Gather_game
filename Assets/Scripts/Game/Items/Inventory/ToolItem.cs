using UnityEngine;
using SpellStone.Inventory;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/New Tool Item")]
public class ToolItem : InventoryItem
{
  [Header("Tool Item")]
  public ToolType toolType;
  public ToolAttributes toolAttributes;

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

  public override string GetTooltipContent()
  {
    return base.GetTooltipContent() + "\n" + "Type: " + toolType.ToString() + "\n" +
           "Exp Gain (%): " + toolAttributes.expGainIncrease + "\n";
  }

  public ToolAttributes GetToolAttributes()
  {
    return toolAttributes;
  }
}

public enum ToolType
{
  Axe,
  Pickaxe,
}

[System.Serializable]
public class ToolAttributes
{
  public float expGainIncrease;

  public ToolAttributes(float expGainIncrease)
  {
    this.expGainIncrease = expGainIncrease;
  }
}
