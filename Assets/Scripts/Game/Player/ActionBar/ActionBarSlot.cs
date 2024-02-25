using UnityEngine;
using UnityEngine.UI;
using SpellStone.Inventory;

public class ActionBarSlot : InventorySlot
{
  public TMPro.TextMeshProUGUI slotText;
  private Color originalSlotColor;
  private Image slotImage;

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

  public void HighlightSlot()
  {
    CheckForReferences();
    slotImage.color = Color.yellow;
  }

  public void UnhighlightSlot()
  {
    slotImage.color = originalSlotColor;
  }

  private void CheckForReferences()
  {
    if (slotImage == null)
      slotImage = GetComponent<Image>();

    if (originalSlotColor == default)
      originalSlotColor = slotImage.color;
  }
}
