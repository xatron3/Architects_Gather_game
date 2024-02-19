using UnityEngine;
using UnityEngine.EventSystems;

namespace SpellStone.Inventory
{
  public class InventorySlot : MonoBehaviour, IInventorySlotHander
  {
    public InventoryGrid parentGrid;

    public virtual void AddItem(InventoryItem newItem, InventoryItemPrefab itemIconPrefab, int quantity = 1)
    {
      // Check if the item is stackable and if it's already in the slot
      if (newItem.isStackable && !IsSlotEmpty() && transform.GetChild(0).GetComponent<InventoryItemPrefab>().GetItem().itemName == newItem.itemName)
      {
        transform.GetChild(0).GetComponent<InventoryItemPrefab>().GetItem().currentStackSize += quantity;
        transform.GetChild(0).GetComponent<InventoryItemPrefab>().UpdateStackSize();
        return;
      }

      InventoryItemPrefab itemPrefab = Instantiate(itemIconPrefab, transform);
      newItem.SetSlotIndex(GetSlotIndex());
      itemPrefab.SetItem(newItem);
      parentGrid.items.Add(newItem);
    }

    public virtual void ClearSlot()
    {
      if (!IsSlotEmpty())
      {
        DestroyImmediate(transform.GetChild(0).gameObject);
      }
    }

    public virtual bool IsSlotEmpty()
    {
      return transform.childCount == 0;
    }

    public virtual void HandleItemDrop(InventoryItemPrefab itemPrefab, PointerEventData eventData)
    {
      if (!CanDropItem(eventData))
      {
        return;
      }

      if (DropItem(itemPrefab, eventData))
      {
        Debug.Log("Item dropped successfully");
      }
    }

    public bool CanDropItem(PointerEventData eventData)
    {
      if (transform.childCount > 0)
      {
        Debug.Log("Slot is already occupied");
        return false; // Drop failed if the slot is already occupied
      }

      if (eventData.pointerCurrentRaycast.gameObject == null)
      {
        Debug.Log("Dropped item on empty space");
        return false; // Drop failed if the item was dropped on empty space
      }

      return true;
    }

    public virtual bool DropItem(InventoryItemPrefab itemPrefab, PointerEventData eventData)
    {
      Transform droppedTransform = eventData.pointerCurrentRaycast.gameObject.GetComponent<InventorySlot>()?.transform;

      if (droppedTransform == null)
      {
        Debug.Log("Dropped item on non-slot object");
        return false; // Drop failed if the item was dropped on a non-slot object
      }

      InventorySlot droppedSlot = droppedTransform.GetComponent<InventorySlot>();
      InventorySlotVisitor visitor = new InventorySlotVisitor(itemPrefab);

      droppedSlot.Accept(visitor);

      return visitor.DropSuccessful;
    }

    public void SetParentGrid(InventoryGrid grid)
    {
      parentGrid = grid;
    }

    public int GetSlotIndex()
    {
      return transform.GetSiblingIndex();
    }

    public virtual void Accept(ISlotVisitor visitor)
    {
      visitor.Visit(this);
    }
  }
}