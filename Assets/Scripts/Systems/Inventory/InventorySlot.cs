using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SpellStone.Inventory
{
  public class InventorySlot : MonoBehaviour, IDropHandler
  {
    public InventoryItemPrefab itemPrefab;

    public void AddItem(InventoryItem newItem)
    {
      InventoryItemPrefab itemIconPrefab = Resources.Load<InventoryItemPrefab>("Prefabs/Player/Inventory/InventoryItemPrefab");

      itemPrefab = Instantiate(itemIconPrefab, transform);
      itemPrefab.item = newItem;
      itemPrefab.icon.sprite = newItem.icon;
      itemPrefab.icon.enabled = true;
    }

    public void ClearSlot()
    {
    }

    public void OnDrop(PointerEventData eventData)
    {
      GameObject droppedItem = eventData.pointerDrag;

      if (droppedItem != null)
      {
        InventoryItemPrefab InventoryItemPrefab = droppedItem.GetComponent<InventoryItemPrefab>();
        if (InventoryItemPrefab != null)
        {
          InventoryItemPrefab.parentToReturnTo = transform;
        }
      }
    }
  }
}