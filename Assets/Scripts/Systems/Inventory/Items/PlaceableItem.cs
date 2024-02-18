using UnityEngine;
using SpellStone.Inventory;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/New Placeable Item")]
public class PlaceOnGroundItem : InventoryItem
{
  [Header("Placeable Item")]
  public GameObject itemToPlace;

  public override void Use()
  {
    base.Use();
    Debug.Log("Placing " + itemName + " on the ground");
  }
}
