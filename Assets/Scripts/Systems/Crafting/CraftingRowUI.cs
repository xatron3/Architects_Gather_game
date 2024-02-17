using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;

namespace SpellStone.Crafting
{
  public class CraftingRowUI : MonoBehaviour
  {
    public event Action<CraftingItem> PreviewCraftEvent; // Define an event

    private CraftingItem craftingItem;
    public Image itemIcon;
    public TMP_Text itemName;
    private Button button;

    private void Awake()
    {
      button = GetComponent<Button>();
      button.onClick.AddListener(PreviewCraft);
    }

    private void PreviewCraft()
    {
      PreviewCraftEvent?.Invoke(craftingItem);
    }

    public void SetCraftingItem(CraftingItem item)
    {
      craftingItem = item;
      SetIcon(item.craftableItem.icon);
      SetName(item.craftingName);
    }

    private void SetIcon(Sprite icon)
    {
      itemIcon.sprite = icon;
    }

    private void SetName(string name)
    {
      itemName.text = name;
    }
  }
}
