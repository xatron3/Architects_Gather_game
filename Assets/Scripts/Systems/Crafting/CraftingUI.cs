using System.Collections.Generic;
using UnityEngine;

namespace SpellStone.Crafting
{
  public class CraftingUI : MonoBehaviour
  {
    public CraftingRowUI craftingRowPrefab;
    public Transform ItemsToCraftContainerTransform;

    public void SetupUI(List<CraftingItem> craftingItems)
    {
      foreach (CraftingItem item in craftingItems)
      {
        CraftingRowUI row = Instantiate(craftingRowPrefab, ItemsToCraftContainerTransform);
        row.SetCraftingItem(item);
      }
    }
  }
}