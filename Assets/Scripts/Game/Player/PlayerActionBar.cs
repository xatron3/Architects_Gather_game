using UnityEngine;
using SpellStone.ActionBar;
using SpellStone.Inventory;

public class PlayerActionBar : MonoBehaviour
{
  public InventoryGrid actionBarGrid;
  public InventorySlot actionBarSlotPrefab;
  public IInventorySlotHander actionBarSlot;
  private InventoryGrid playerActionBarGrid;

  private InventoryItemPrefab itemIconPrefab;
  public InventoryItem equppedItem;

  void Start()
  {
    GameObject UI_Canvas_GO = GetComponentInChildren<Canvas>().gameObject;

    if (UI_Canvas_GO != null)
    {
      playerActionBarGrid = Instantiate(actionBarGrid, transform);
      playerActionBarGrid.transform.SetParent(UI_Canvas_GO.transform.Find("Container").transform, false);

      itemIconPrefab = Resources.Load<InventoryItemPrefab>("Prefabs/Player/Inventory/UI_InventoryItem");

      playerActionBarGrid.InitializeGrid(8, actionBarSlotPrefab, playerActionBarGrid.transform);

      playerActionBarGrid.gameObject.SetActive(true);
    }
  }

  void Update()
  {
    // Handle keyboard input for item equipping
    for (int i = 0; i < playerActionBarGrid.slots.Count; i++)
    {
      if (Input.GetKeyDown(KeyCode.Alpha1 + i)) // Assuming keys 1-8
      {
        if (equppedItem != null)
          UnequipItem();

        EquipItem(i);
      }
    }

    // Handle mouse input for equipped item usage
    if (Input.GetMouseButtonDown(0))
    {
      if (equppedItem != null)
      {
        // If the equipped item is a placeable item, place it on the ground in front of the player
        if (equppedItem is PlaceOnGroundItem)
        {
          PlaceOnGroundItem placeableItem = (PlaceOnGroundItem)equppedItem.GetCopy();
          // Set the Y position of the item to be at 0 but in front of the player
          Vector3 itemPosition = new Vector3(transform.position.x, placeableItem.itemToPlace.transform.position.y, transform.position.z) + transform.forward;

          PlayerPlacedItem placedItem = Instantiate(placeableItem.itemToPlace, itemPosition, placeableItem.itemToPlace.transform.rotation);

          placedItem.transform.SetParent(PlayerPlacedItems.Instance.itemsParent);
          PlayerPlacedItems.Instance.items.Add(placedItem);

          // Remove the item from the player's action bar grid
          playerActionBarGrid.RemoveItem(equppedItem);

          UnequipItem();
        }
        else
        {
          equppedItem.Use();
        }
      }
    }
  }

  public bool AddItem(InventoryItem item)
  {
    if (playerActionBarGrid.AddItem(item, itemIconPrefab))
    {
      return true;
    }

    return false;
  }

  private void EquipItem(int slotIndex)
  {
    ActionBarSlot slot = playerActionBarGrid.slots[slotIndex] as ActionBarSlot;
    InventoryItemPrefab itemPrefab = slot.GetComponentInChildren<InventoryItemPrefab>();

    if (itemPrefab != null)
    {
      equppedItem = itemPrefab.GetItem();
    }
  }

  private void UnequipItem()
  {
    equppedItem = null;
  }
}
