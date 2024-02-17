using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SpellStone.Inventory
{
  public class InventoryItemPrefab : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
  {
    [HideInInspector] public Transform parentToReturnTo = null;
    public Image icon { get; private set; }
    public InventoryItem item { get; private set; }

    public void UseItem()
    {
      if (item != null)
      {
        item.Use();
      }
    }

    public void SetIconSprite(Sprite sprite)
    {
      if (icon == null)
        icon = GetComponent<Image>();

      icon.sprite = sprite;
    }

    public InventoryItem GetItem()
    {
      if (item != null)
      {
        return item;
      }
      else
      {
        return null;
      }
    }

    public void SetItem(InventoryItem newItem)
    {
      item = newItem;
      Debug.Log("Setting item: " + item.itemName);
      SetIconSprite(item.icon);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
      parentToReturnTo = transform.parent;
      transform.SetParent(transform.parent.parent);
      transform.SetAsLastSibling();
      icon.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
      transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      transform.SetParent(parentToReturnTo, true);
      icon.raycastTarget = true;
    }
  }
}