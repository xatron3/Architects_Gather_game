using UnityEngine;
using SpellStone.Inventory;
using System.Collections.Generic;

public class PlayerActionBar : MonoBehaviour
{
  public InventoryGrid actionBarGrid;
  public InventorySlot actionBarSlotPrefab;
  public IInventorySlotHander actionBarSlot;
  private InventoryGrid playerActionBarGrid;

  public InventoryItemPrefab inventoryItemPrefab;

  public InventoryItem equppedItem;
  private int equippedItemSlotIndex;

  void Start()
  {
    SetupActionBarGrid();

    LoadSavedActionBarItems();
  }

  void OnApplicationQuit()
  {
    SaveLoadManager.SavePlayerActionbar(playerActionBarGrid.GetItems());
  }

  void Update()
  {
    for (int i = 0; i < playerActionBarGrid.slots.Count; i++)
    {
      if (Input.GetKeyDown(KeyCode.Alpha1 + i)) // Assuming keys 1-8
      {
        if (equppedItem != null)
        {
          UnequipItem();
        }

        EquipItem(i);
      }
    }

    if (Input.GetMouseButtonDown(0))
    {
      if (equppedItem != null)
      {
        equppedItem.Use(GetComponent<PlayerController>());

        if (equppedItem.breakOnUse)
        {
          playerActionBarGrid.RemoveItem(equppedItem, 1);
          UnequipItem();
        }
      }
    }
  }

  public bool AddItem(InventoryItem item)
  {
    if (playerActionBarGrid.AddItem(item, inventoryItemPrefab))
    {
      return true;
    }

    return false;
  }

  private void EquipItem(int slotIndex)
  {
    ActionBarSlot slot = playerActionBarGrid.slots[slotIndex] as ActionBarSlot;
    InventoryItemPrefab itemPrefab = slot.GetComponentInChildren<InventoryItemPrefab>();

    if (itemPrefab != null)
    {
      HighlightSlot(slotIndex);
      equippedItemSlotIndex = slotIndex;
      equppedItem = itemPrefab.GetItem();
      equppedItem.Equip();
    }
  }

  private void UnequipItem()
  {
    UnhighlightSlot();
    equippedItemSlotIndex = -1;
    equppedItem.Unequip();
    equppedItem = null;
  }

  private void HighlightSlot(int slotIndex)
  {
    ActionBarSlot slot = playerActionBarGrid.slots[slotIndex] as ActionBarSlot;
    slot.HighlightSlot();
  }

  private void UnhighlightSlot()
  {
    ActionBarSlot slot = playerActionBarGrid.slots[equippedItemSlotIndex] as ActionBarSlot;
    slot.UnhighlightSlot();
  }

  private void SetupActionBarGrid()
  {
    GameObject UI_Canvas_GO = GetComponentInChildren<Canvas>().gameObject;

    if (UI_Canvas_GO != null)
    {
      playerActionBarGrid = Instantiate(actionBarGrid, transform);
      playerActionBarGrid.transform.SetParent(UI_Canvas_GO.transform.Find("Container").transform, false);

      playerActionBarGrid.InitializeGrid(8, actionBarSlotPrefab, playerActionBarGrid.transform);

      for (int i = 0; i < playerActionBarGrid.slots.Count; i++)
      {
        ActionBarSlot slot = playerActionBarGrid.slots[i] as ActionBarSlot;
        slot.SetSlotText((i + 1).ToString());
      }

      playerActionBarGrid.gameObject.SetActive(true);
    }
  }

  private void LoadSavedActionBarItems()
  {
    List<InventoryItem> savedItems = SaveLoadManager.LoadPlayerActionbar();
    if (savedItems != null)
    {
      foreach (var item in savedItems)
      {
        InventoryItemPrefab newItemPrefab = playerActionBarGrid.AddItem(item, inventoryItemPrefab, item.currentStackSize, true, item.slotIndex);

        if (newItemPrefab != null)
        {
          newItemPrefab.transform.SetAsFirstSibling();
        }
      }
    }
  }
}