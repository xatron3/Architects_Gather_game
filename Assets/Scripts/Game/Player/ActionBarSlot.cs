using UnityEngine;
using UnityEngine.EventSystems;
using SpellStone.Inventory;

namespace SpellStone.ActionBar
{
  public class ActionBarSlot : InventorySlot
  {
    public TMPro.TextMeshProUGUI slotText;

    public void SetSlotText(string text)
    {
      slotText.text = text;
    }
  }
}
