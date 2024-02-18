using SpellStone.Crafting;
using UnityEngine;
using System.Collections.Generic;

public class CraftingTable : MonoBehaviour, IInteractable
{
  public List<CraftingItem> craftingItems = new List<CraftingItem>();
  public CraftingUI craftingTableUIPrefab;
  private CraftingUI craftingTableUI;

  private PlayerSkills playerSkills;
  private PlayerInventory playerInventory;

  public string Tooltip => "Open crafting table";

  public void Interact()
  {
    // Find the player skills and inventory
    if (playerSkills == null)
      playerSkills = FindObjectOfType<PlayerSkills>();

    if (playerInventory == null)
      playerInventory = FindObjectOfType<PlayerInventory>();

    Transform UI_Container = GameObject.Find("UI_Elements/Container").transform;
    craftingTableUI = Instantiate(craftingTableUIPrefab, UI_Container, false);

    craftingTableUI.SetupUI(craftingItems);

    // Find the crafting preview UI and set the player skills and inventory
    CraftingPreviewUI craftingPreviewUI = craftingTableUI.GetComponentInChildren<CraftingPreviewUI>();
    craftingPreviewUI.SetPlayerSkills(playerSkills);
    craftingPreviewUI.SetPlayerInventory(playerInventory);
    craftingPreviewUI.SetupUI();
    craftingPreviewUI.AddEventListners();
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      if (craftingTableUI != null)
      {
        Destroy(craftingTableUI.gameObject);
        craftingTableUI = null;
      }
    }
  }
}