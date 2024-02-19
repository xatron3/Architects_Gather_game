using UnityEngine;
using UnityEngine.EventSystems;
using SpellStone.Inventory;

public class ActionBarSlot : InventorySlot
{
  public TMPro.TextMeshProUGUI slotText;

  public void SetSlotText(string text)
  {
    slotText.text = text;
  }

  public override void Accept(ISlotVisitor visitor)
  {
    visitor.Visit(this);
  }

  public override bool IsSlotEmpty()
  {
    return transform.childCount == 1;
  }
}
