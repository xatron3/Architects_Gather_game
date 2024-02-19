using System.Collections.Generic;
using SpellStone.Inventory;
using UnityEngine;

public class StorageChest : PlayerPlacedItem, IInteractable
{
  public override string ItemName => "Storage Chest";

  public InventoryGrid playerStorageUIPrefab;
  public InventorySlot playerStorageSlotPrefab;
  public IInventorySlotHander playerStorageSlot;

  private InventoryGrid playerStorageUI;
  public List<InventoryItem> storageChestItems = new List<InventoryItem>();

  public string Tooltip => "Open player storage";

  public void Interact()
  {
    Transform UI_Container = GameObject.Find("UI_Elements/Container").transform;

    playerStorageUI = Instantiate(playerStorageUIPrefab, UI_Container, false);

    playerStorageUI.InitializeGrid(20, playerStorageSlotPrefab, playerStorageUI.transform.Find("Container/ItemsGridList").transform);

    foreach (InventoryItem item in storageChestItems)
    {
      playerStorageUI.AddItem(item, Resources.Load<InventoryItemPrefab>("Prefabs/Player/Inventory/UI_InventoryItem"), item.currentStackSize, true);
    }

    foreach (var itemPrefab in playerStorageUI.GetComponentsInChildren<InventoryItemPrefab>())
    {
      itemPrefab.OnItemDroppedToInventory += RemoveItem;
    }
  }

  public void AddItem(InventoryItem item)
  {
    // Check if the item is already in the storage
    if (!storageChestItems.Exists(i => i.GetUniqueID() == item.GetUniqueID()))
    {
      storageChestItems.Add(item);
    }
  }

  public void RemoveItem(InventoryItem item)
  {
    // Remove the item from the storage based on its unique identifier
    InventoryItem itemToRemove = storageChestItems.Find(i => i.GetUniqueID() == item.GetUniqueID());
    if (itemToRemove != null)
    {
      storageChestItems.Remove(itemToRemove);
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      if (playerStorageUI != null)
      {
        storageChestItems = playerStorageUI.items;
        Destroy(playerStorageUI.gameObject);
        playerStorageUI = null;
      }
    }
  }
}
