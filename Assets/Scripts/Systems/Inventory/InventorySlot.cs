using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections.Generic;
using SpellStone.ActionBar;

namespace SpellStone.Inventory
{
  public class InventorySlot : MonoBehaviour, IInventorySlotHander
  {
    private InventoryGrid parentGrid;

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

      // Based on the eventData we know what slot the item was dropped on
      InventorySlot droppedSlot = droppedTransform.GetComponent<InventorySlot>();
      InventorySlot previousSlot = itemPrefab.parentToReturnTo.GetComponent<InventorySlot>();

      // Check if the dropped slot is an ActionBarSlot and the item can't be equipped
      if (droppedSlot is ActionBarSlot && !itemPrefab.GetItem().canEquip)
      {
        Debug.Log("Cannot equip this item to ActionBar because canEquip is false.");
        return false;
      }

      droppedSlot.parentGrid.items.Add(itemPrefab.GetItem());
      previousSlot.parentGrid.items.Remove(itemPrefab.GetItem());

      itemPrefab.GetItem().SetSlotIndex(droppedSlot.GetSlotIndex());

      Debug.Log("Dropping item to: " + droppedTransform.name);
      itemPrefab.parentToReturnTo = droppedTransform;
      itemPrefab.transform.SetParent(droppedTransform); // Set the parent of the dropped item to the new slot
      itemPrefab.transform.localPosition = Vector3.zero; // Reset local position
      return true; // Drop successful
    }

    public void SetParentGrid(InventoryGrid grid)
    {
      parentGrid = grid;
    }

    public int GetSlotIndex()
    {
      return transform.GetSiblingIndex();
    }
  }
}