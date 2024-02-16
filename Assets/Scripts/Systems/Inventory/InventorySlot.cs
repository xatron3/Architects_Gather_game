using UnityEngine;
using UnityEngine.UI;

namespace SpellStone.Inventory
{
  public class InventorySlot : MonoBehaviour
  {
    public Image icon;
    public InventoryItem item;

    public void AddItem(InventoryItem newItem)
    {
      item = newItem;
      icon.sprite = item.icon;
      icon.enabled = true;
    }

    public void ClearSlot()
    {
      item = null;
      icon.sprite = null;
      icon.enabled = false;
    }

    public void UseItem()
    {
      ClearSlot();
    }

    public void DropItem()
    {
      ClearSlot();
    }
  }
}