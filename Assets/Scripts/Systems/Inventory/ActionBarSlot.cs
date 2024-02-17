// ActionBarSlot.cs
using UnityEngine;
using UnityEngine.EventSystems;

namespace SpellStone.Inventory
{
  public class ActionBarSlot : MonoBehaviour, IDropHandler
  {
    public void AddItem(InventoryItem newItem)
    {
      InventoryItemPrefab itemIconPrefab = Resources.Load<InventoryItemPrefab>("Prefabs/Player/ActionBar/ActionBarItemPrefab");

      InventoryItemPrefab itemPrefab = Instantiate(itemIconPrefab, transform);
      itemPrefab.SetItem(newItem);
    }

    public void RemoveItem()
    {
    }

    public void OnDrop(PointerEventData eventData)
    {
      GameObject droppedItem = eventData.pointerDrag;

      if (droppedItem != null)
      {
        InventoryItemPrefab inventoryItemPrefab = droppedItem.GetComponent<InventoryItemPrefab>();
        if (inventoryItemPrefab != null)
        {
          inventoryItemPrefab.parentToReturnTo = transform;
        }
      }
    }
  }
}
