using UnityEngine;
using System.Collections.Generic;
using SpellStone.Inventory;

public class PlayerInventory : MonoBehaviour, IPlayerInventory
{
  public InventoryGrid inventoryGridPrefab;
  private InventoryGrid inventoryGrid;
  private InventoryItemPrefab itemIconPrefab;

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

      ItemEventManager.Instance.onItemPickedUp.AddListener(OnItemPickedUp);
      itemIconPrefab = Resources.Load<InventoryItemPrefab>("Prefabs/Player/Inventory/UI_InventoryItemPrefab");

      inventoryGrid.gameObject.SetActive(false);
    }
    else
      Debug.LogError("UI_Canvas not found. Could not create inventory grid.");
  }

  public void AddItem(InventoryItem item)
  {
    inventoryGrid.AddItem(item, itemIconPrefab);
  }

  public void RemoveItem(InventoryItem item)
  {
    Debug.Log("Removing item: " + item.itemName + " from player inventory");
    inventoryGrid.RemoveItem(item);
  }

  public int GetTotalItemsOfName(string itemName)
  {
    return inventoryGrid.GetTotalItemsOfName(itemName);
  }

  public bool HasIngredients(List<InventoryItem> ingredients)
  {
    foreach (InventoryItem ingredient in ingredients)
    {
      if (GetTotalItemsOfName(ingredient.itemName) < 1)
      {
        return false;
      }
    }

    return true;
  }

  public void RemoveIngredients(List<InventoryItem> ingredients)
  {
    foreach (InventoryItem ingredient in ingredients)
    {
      RemoveItem(ingredient);
    }
  }
}