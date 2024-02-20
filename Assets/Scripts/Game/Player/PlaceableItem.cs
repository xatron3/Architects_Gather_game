using UnityEngine;
using SpellStone.Inventory;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/New Placeable Item")]
public class PlaceOnGroundItem : InventoryItem
{
  [Header("Placeable Item")]
  public PlayerPlacedItem itemToPlace;

  public override void Use(PlayerController player)
  {
    base.Use(player);
    Debug.Log("Placing " + itemName + " on the ground");

    PlaceItem(player);
  }

  private void PlaceItem(PlayerController player)
  {
    PlaceOnGroundItem placeableItem = (PlaceOnGroundItem)GetCopy();
    // Set the Y position of the item to be at 0 but in front of the player
    Vector3 itemPosition = new Vector3(player.transform.position.x, placeableItem.itemToPlace.transform.position.y, player.transform.position.z) + player.transform.forward;

    PlayerPlacedItem placedItem = Instantiate(placeableItem.itemToPlace, itemPosition, placeableItem.itemToPlace.transform.rotation);

    placedItem.transform.SetParent(PlayerPlacedItems.Instance.itemsParent);
    PlayerPlacedItems.Instance.items.Add(placedItem);
  }
}