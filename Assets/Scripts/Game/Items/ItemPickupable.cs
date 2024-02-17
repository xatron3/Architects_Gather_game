using SpellStone.Inventory;
using UnityEngine;

public class ItemPickupable : Item, IInteractable
{
  public InventoryItem inventoryItem;

  public void Interact()
  {
    Pickup();
  }

  public string Tooltip
  {
    get { return "Pick up " + inventoryItem.itemName; }
  }

  private void Pickup()
  {
    ItemEventManager.Instance.onItemPickedUp.Invoke(this);
  }
}
