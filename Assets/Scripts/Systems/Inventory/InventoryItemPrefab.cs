using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SpellStone.Inventory
{
  public class InventoryItemPrefab : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
  {
    [HideInInspector] public Transform parentToReturnTo = null;
    public Image icon;
    public InventoryItem item;

    public void UseItem()
    {
      if (item != null)
      {
        item.Use();
      }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
      Debug.Log("OnBeginDrag");
      parentToReturnTo = transform.parent;
      transform.SetParent(transform.parent.parent);
      transform.SetAsLastSibling();
      icon.raycastTarget = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
      Debug.Log("OnDrag");
      transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
      Debug.Log("OnEndDrag");
      transform.SetParent(parentToReturnTo, true);
      icon.raycastTarget = true;
    }
  }
}