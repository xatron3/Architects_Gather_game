using UnityEngine;
using SpellStone.Inventory;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

namespace SpellStone.Crafting
{
  public class CraftingPreviewUI : MonoBehaviour
  {
    public CraftingPreviewUIElements elements;

    private List<CraftingRowUI> craftingRowsList = new List<CraftingRowUI>();

    private void Start()
    {
      CraftingRowUI[] craftingRows = transform.parent.Find("LeftSideHolder").Find("ItemsToCraftContainer").GetComponentsInChildren<CraftingRowUI>();

      foreach (CraftingRowUI craftingRow in craftingRows)
      {
        craftingRow.PreviewCraftEvent += OnPreviewCraft;
        craftingRowsList.Add(craftingRow);
      }
    }

    private void OnDestroy()
    {
      foreach (CraftingRowUI craftingRow in craftingRowsList)
      {
        craftingRow.PreviewCraftEvent -= OnPreviewCraft;
      }
    }

    private void OnPreviewCraft(CraftingItem craftingItem)
    {
      ClearPreviewContainer();

      foreach (InventoryItem ingredient in craftingItem.ingredients)
      {
        GameObject row = Instantiate(elements.materialsRowPrefab, elements.materialsContainer);
        row.GetComponent<CraftingMaterialRowUI>().SetupRow(ingredient);
      }

      elements.craftButton.onClick.AddListener(() => CheckAndCraft(craftingItem));
    }

    private void CheckAndCraft(CraftingItem item)
    {
      PlayerInventory playerInventory = FindObjectOfType<PlayerInventory>();

      if (playerInventory != null)
      {
        if (playerInventory.HasIngredients(item.ingredients))
        {
          playerInventory.RemoveIngredients(item.ingredients);
          playerInventory.AddItem(item.craftableItem);
        }
        else
        {
          Debug.Log("PlayerInventory does not have the required ingredients to craft this item.");
        }
      }
      else
      {
        Debug.LogError("PlayerInventory not found. Could not craft item.");
      }
    }

    private void ClearPreviewContainer()
    {
      elements.craftButton.onClick.RemoveAllListeners();

      foreach (Transform child in elements.materialsContainer)
      {
        Destroy(child.gameObject);
      }
    }
  }

  [Serializable]
  public class CraftingPreviewUIElements
  {
    public Transform materialsContainer;
    public GameObject materialsRowPrefab;
    public Button craftButton;
  }
}
