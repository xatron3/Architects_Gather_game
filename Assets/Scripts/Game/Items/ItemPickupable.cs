using SpellStone.Inventory;
using UnityEngine;

public class ItemPickupable : Item, IInteractable
{
  public float InteractionRadius => 3f;
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

  public void OnMoveOutOfRange()
  {
    // Do nothing
  }

  public Vector3 GetPosition()
  {
    return transform.position;
  }

  public void OnDrawGizmos()
  {
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, InteractionRadius);
  }
}
