using System.Collections.Generic;
using UnityEngine;

namespace SpellStone.Crafting
{
  public class CraftingUI : MonoBehaviour
  {
    public List<CraftingItem> craftingItems = new List<CraftingItem>();
    public CraftingRowUI craftingRowPrefab;

    public Transform ItemsToCraftContainerTransform;

    private void Start()
    {

      foreach (CraftingItem item in craftingItems)
      {
        CraftingRowUI row = Instantiate(craftingRowPrefab, ItemsToCraftContainerTransform);
        row.SetCraftingItem(item);
      }
    }
  }
}