using UnityEngine;
using SpellStone.Inventory;

[CreateAssetMenu(fileName = "New Item", menuName = "Items/New Placeable Item")]
public class PlaceOnGroundItem : InventoryItem
{
  [Header("Placeable Item")]
  public PlayerPlacedItem itemToPlace;
  private PlayerPlacedItem previewItem;

  public override void Use(PlayerController player)
  {
    base.Use(player);
    Debug.Log("Placing " + itemName + " on the ground");

    if (previewItem != null)
    {
      PlaceItem(player, previewItem);
      Destroy(previewItem.gameObject); // Destroy the preview item after placing
    }
    else
    {
      Debug.LogError("No preview item available.");
    }
  }

  private void PlaceItem(PlayerController player, PlayerPlacedItem itemToPlace)
  {
    RaycastHit hit;
    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

    if (Physics.Raycast(ray, out hit, Mathf.Infinity))
    {
      PlayerPlacedItem placedItem = Instantiate(itemToPlace, itemToPlace.transform.position, itemToPlace.transform.rotation);

      placedItem.SetIsPreview(false);
      placedItem.transform.SetParent(PlayerPlacedItems.Instance.itemsParent);

      PlayerPlacedItems.Instance.items.Add(placedItem);
    }
  }

  public override void Equip()
  {
    previewItem = Instantiate(itemToPlace);
    previewItem.SetIsPreview(true);
  }

  public override void Unequip()
  {
    if (previewItem != null)
    {
      Destroy(previewItem.gameObject);
      previewItem = null; // Reset preview item
    }
  }
}
