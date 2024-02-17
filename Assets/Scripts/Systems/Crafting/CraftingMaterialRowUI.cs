using UnityEngine;
using SpellStone.Inventory;
using UnityEngine.UI;
using TMPro;

namespace SpellStone.Crafting
{
  public class CraftingMaterialRowUI : MonoBehaviour
  {
    public Image imageIcon;
    public TMP_Text nameText;

    public void SetupRow(InventoryItem material)
    {
      SetIcon(material.icon);
      SetName(material.itemName);
    }

    private void SetIcon(Sprite icon)
    {
      imageIcon.sprite = icon;
    }

    private void SetName(string name)
    {
      nameText.text = name;
    }
  }
}
