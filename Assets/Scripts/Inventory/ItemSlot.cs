using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
  public Image icon;
  public Item item;

  public void AddItem(Item newItem)
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