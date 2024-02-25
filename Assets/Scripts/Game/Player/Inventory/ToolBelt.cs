using UnityEngine;
using SpellStone.Inventory;
using System.Collections.Generic;

public class ToolBelt : MonoBehaviour
{
  public InventoryGrid toolBeltGridPrefab;
  public InventoryItemPrefab itemIconPrefab;
  public ToolBeltSlot toolBeltSlotPrefab;

  private InventoryGrid toolBeltGrid;

  void OnApplicationQuit()
  {
    SaveLoadManager.SavePlayerToolbelt(toolBeltGrid.GetItems());
  }

  public void InitializeToolBelt(int slotCount, Transform parent)
  {
    toolBeltGrid = Instantiate(toolBeltGridPrefab, parent, false);
    toolBeltGrid.InitializeGrid(slotCount, toolBeltSlotPrefab, toolBeltGrid.transform);
    LoadSavedToolBeltItems();
  }

  public List<InventoryItem> GetItems()
  {
    return toolBeltGrid.GetItems();
  }

  private void LoadSavedToolBeltItems()
  {
    List<InventoryItem> savedItems = SaveLoadManager.LoadPlayerToolbelt();

    if (savedItems != null)
    {
      foreach (InventoryItem item in savedItems)
      {
        toolBeltGrid.AddItem(item, itemIconPrefab);
      }
    }
  }
}