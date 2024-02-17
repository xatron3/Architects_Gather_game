using UnityEngine;
using SpellStone.Inventory;

namespace SpellStone.Crafting
{
  public class CraftingPreviewUI : MonoBehaviour
  {
    private void Start()
    {
      CraftingRowUI[] craftingRows = transform.parent.Find("LeftSideHolder").Find("ItemsToCraftContainer").GetComponentsInChildren<CraftingRowUI>();

      foreach (CraftingRowUI craftingRow in craftingRows)
      {
        craftingRow.PreviewCraftEvent += OnPreviewCraft;
      }
    }

    private void OnDestroy()
    {
      CraftingRowUI[] craftingRows = GetComponentsInChildren<CraftingRowUI>();

      foreach (CraftingRowUI craftingRow in craftingRows)
      {
        craftingRow.PreviewCraftEvent -= OnPreviewCraft;
      }
    }

    private void OnPreviewCraft(CraftingItem craftingItem)
    {
      Debug.Log("Previewing craft: " + craftingItem.craftingName);

      foreach (InventoryItem ingredient in craftingItem.ingredients)
      {
        Debug.Log("Ingredient: " + ingredient.name);
      }
    }
  }
}
