using UnityEngine;
using UnityEngine.EventSystems;
using SpellStone.Inventory;

namespace SpellStone.ActionBar
{
  public class ActionBarSlot : MonoBehaviour, IDropHandler
  {
    public TMPro.TextMeshProUGUI slotText;

    public void SetSlotText(string text)
    {
      slotText.text = text;
    }

    public void AddItem(InventoryItem newItem, InventoryItemPrefab itemIconPrefab)
    {
      InventoryItemPrefab itemPrefab = Instantiate(itemIconPrefab, transform.Find("ItemContainer").transform, false);
      itemPrefab.SetItem(newItem);
    }

    public void ClearSlot()
    {
      if (IsSlotEmpty() == false)
      {
        DestroyImmediate(transform.Find("ItemContainer").transform.GetChild(0).gameObject);
      }
    }

    public bool IsSlotEmpty()
    {
      if (transform.Find("ItemContainer").transform.childCount > 0)
        return false;

      return true;
    }

    public void OnDrop(PointerEventData eventData)
    {
      if (IsSlotEmpty() == false)
      {
        Debug.Log("Slot is not empty");
        return;
      }

      GameObject droppedItem = eventData.pointerDrag;

      if (droppedItem != null)
      {
        InventoryItemPrefab inventoryItemPrefab = droppedItem.GetComponent<InventoryItemPrefab>();
        if (inventoryItemPrefab != null)
        {
          InventoryItem item = inventoryItemPrefab.GetItem();

          if (item.canEquip)
            inventoryItemPrefab.parentToReturnTo = transform.Find("ItemContainer").transform;
        }
      }
    }
  }
}
