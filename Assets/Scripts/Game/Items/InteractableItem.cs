using SpellStone.Inventory;
using UnityEngine;

public class InteractableItem : Item, IInteractable
{
  public InventoryItem item;

  public void Interact()
  {
    Pickup();
  }

  public string Tooltip
  {
    get { return "Pick up " + item.itemName; }
  }

  private void Pickup()
  {
    ItemEventManager.Instance.onItemPickedUp.Invoke(item);
    Destroy(gameObject);
  }
}
