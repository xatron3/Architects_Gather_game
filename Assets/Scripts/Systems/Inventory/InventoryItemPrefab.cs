using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace SpellStone.Inventory
{
  public class InventoryItemPrefab : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
  {
    [HideInInspector] public Transform parentToReturnTo = null;
    public Image icon { get; private set; }
    public InventoryItem item;

    private void Awake()
    {
      icon = GetComponent<Image>();
    }

    public void UseItem()
    {
      if (item != null)
      {
        item.Use();
      }
    }

    public void SetIconSprite(Sprite sprite)
    {
      icon.sprite = sprite;
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