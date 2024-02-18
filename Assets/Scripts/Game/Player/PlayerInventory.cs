using UnityEngine;
using System.Collections.Generic;
using SpellStone.Inventory;

public class PlayerInventory : MonoBehaviour, IPlayerInventory
{
  public InventoryGrid inventoryGridPrefab;
  public InventorySlot inventorySlotPrefab;

  private InventoryGrid inventoryGrid;
  public InventoryItemPrefab itemIconPrefab;

  private InventoryManager inventoryManager = new();

  private void Start()
  {
    CreateInventoryGrid();
  }

  private void OnDestroy()
  {
    ItemEventManager.Instance.onItemPickedUp.RemoveListener(OnItemPickedUp);
  }

  private void OnItemPickedUp(ItemPickupable item)
  {
    if (inventoryGrid.GetTotalItems() < inventoryGrid.slots.Count)
    {
      AddItem(item.inventoryItem);
      Destroy(item.gameObject);
    }
    else
      Debug.Log("Inventory is full. Cannot add item: " + item.inventoryItem.itemName);
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.B))
    {
      // Toggle the inventory grid when pressing "B"
      inventoryGrid.gameObject.SetActive(!inventoryGrid.gameObject.activeSelf);
    }
  }

  private void CreateInventoryGrid()
  {
    GameObject UI_Canvas_GO = GetComponentInChildren<Canvas>().gameObject;

    if (UI_Canvas_GO != null)
    {
      inventoryGrid = Instantiate(inventoryGridPrefab, inventoryGridPrefab.transform.position, Quaternion.identity);
      inventoryGrid.transform.SetParent(UI_Canvas_GO.transform.Find("Container").transform, false);

      inventoryGrid.InitializeGrid(12, inventorySlotPrefab, inventoryGrid.transform);

      ItemEventManager.Instance.onItemPickedUp.AddListener(OnItemPickedUp);
      itemIconPrefab = Resources.Load<InventoryItemPrefab>("Prefabs/Player/Inventory/UI_InventoryItem");

      inventoryGrid.gameObject.SetActive(false);
    }
    else
      Debug.LogError("UI_Canvas not found. Could not create inventory grid.");
  }

  public bool AddItem(InventoryItem item)
  {
    return inventoryManager.AddItem(inventoryGrid, item.GetCopy(), itemIconPrefab);
  }

  public void RemoveItem(InventoryItem item)
  {
    Debug.Log("Removing item: " + item.itemName + " from player inventory");
    inventoryGrid.RemoveItem(item);
  }

  public void RemoveItems(List<InventoryItem> items)
  {
    foreach (InventoryItem item in items)
    {
      RemoveItem(item);
    }
  }

  public bool ContainsItem(InventoryItem item)
  {
    return GetTotalItemsOfName(item.itemName) > 0;
  }

  public bool ContainsItem(string itemName)
  {
    return GetTotalItemsOfName(itemName) > 0;
  }

  public bool ContainsItems(List<InventoryItem> items)
  {
    foreach (InventoryItem item in items)
    {
      if (GetTotalItemsOfName(item.itemName) < 1)
      {
        return false;
      }
    }

    return true;
  }

  public int GetTotalItemsOfName(string itemName)
  {
    return inventoryGrid.GetTotalItemsOfName(itemName);
  }

  public int GetTotalItems()
  {
    return inventoryGrid.GetTotalItems();
  }

  public int GetFreeSlots()
  {
    return inventoryGrid.GetFreeSlots();
  }
}