using SpellStone.Inventory;
using UnityEngine;

public class InteractableItem : MonoBehaviour, IInteractable
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
    Debug.Log("Picking up " + item.itemName);
    ItemEventManager.Instance.onItemPickedUp.Invoke(item);
    Destroy(gameObject);
  }
}
