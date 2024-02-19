using UnityEngine;
using SpellStone.Inventory;
using System.Collections.Generic;
using System;
using UnityEngine.UI;

namespace SpellStone.Crafting
{
  public class CraftingPreviewUI : MonoBehaviour, ISkillProvider
  {
    private IPlayerSkills playerSkills;
    private IPlayerInventory playerInventory;
    private CraftingItem selectedCraftingItem;
    private List<CraftingRowUI> craftingRowsList = new List<CraftingRowUI>();

    public CraftingPreviewUIElements elements;

    public void SetPlayerSkills(IPlayerSkills playerSkills)
    {
      this.playerSkills = playerSkills;
    }

    public void SetPlayerInventory(IPlayerInventory playerInventory)
    {
      this.playerInventory = playerInventory;
    }

    public void SetupUI()
    {
      CraftingRowUI[] craftingRows = transform.parent.Find("LeftSideHolder").Find("ItemsToCraftContainer").GetComponentsInChildren<CraftingRowUI>();

      foreach (CraftingRowUI craftingRow in craftingRows)
      {
        craftingRowsList.Add(craftingRow);
      }
    }

    public void AddEventListners()
    {
      foreach (CraftingRowUI craftingRow in craftingRowsList)
      {
        craftingRow.PreviewCraftEvent += OnPreviewCraft;
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

      selectedCraftingItem = craftingItem;

      foreach (InventoryItem ingredient in craftingItem.ingredients)
      {
        GameObject row = Instantiate(elements.materialsRowPrefab, elements.materialsContainer);
        row.GetComponent<CraftingMaterialRowUI>().SetupRow(ingredient);
      }

      elements.craftButton.onClick.AddListener(() => CheckAndCraft(craftingItem));
    }

    private void CheckAndCraft(CraftingItem item)
    {
      if (playerSkills.GetSkill("Crafting").GetSkillLevel() < GetRequiredCraftingLevel(item))
      {
        Debug.Log("You need a higher crafting level to craft this item.");
        return;
      }

      CraftItem(item);
    }

    private void CraftItem(CraftingItem item)
    {
      if (playerInventory == null)
      {
        Debug.LogError("PlayerInventory not found. Could not craft item.");
        return;
      }

      if (playerSkills == null)
      {
        Debug.LogError("PlayerSkills not found. Could not increase crafting skill.");
        return;
      }

      if (!playerInventory.ContainsItems(item.ingredients))
      {
        Debug.Log("PlayerInventory does not have the required ingredients to craft this item.");
        return;
      }

      playerInventory.RemoveItems(item.ingredients);
      playerSkills.PerformSkillAction(this);

      if (playerInventory.AddItem(item.craftableItem.GetCopy()))
      {
        Debug.Log("Item crafted and added to inventory");
      }
      else
      {
        Debug.Log("Could not add crafted item to inventory");
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

    public int GetExperienceGain()
    {
      return selectedCraftingItem.craftingExperienceGain;
    }

    public SkillBase GetSkill()
    {
      return new SkillCrafting();
    }

    private int GetRequiredCraftingLevel(CraftingItem item)
    {
      return item.craftingSkillLevelRequirement;
    }

    public int RequiredLevel => GetRequiredCraftingLevel(selectedCraftingItem);

    public InventoryItem RequiredItem { get; set; }

    public bool HasRequiredItem => true;
  }

  [Serializable]
  public class CraftingPreviewUIElements
  {
    public Transform materialsContainer;
    public GameObject materialsRowPrefab;
    public Button craftButton;
  }
}
