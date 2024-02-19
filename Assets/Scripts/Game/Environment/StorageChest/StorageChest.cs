using System.Collections.Generic;
using SpellStone.Inventory;
using UnityEngine;

public class StorageChest : PlayerPlacedItem, IInteractable
{
  public override string ItemName => "Storage Chest";

  public InventoryGrid playerStorageUIPrefab;
  public StorageChestSlot playerStorageSlotPrefab;

  private InventoryGrid playerStorageUI;
  public List<InventoryItem> playerStorageItems = new List<InventoryItem>();

  public string Tooltip => "Open player storage";

  public void Interact()
  {
    Transform UI_Container = GameObject.Find("UI_Elements/Container").transform;

    playerStorageUI = Instantiate(playerStorageUIPrefab, UI_Container, false);

    playerStorageUI.InitializeGrid(20, playerStorageSlotPrefab, playerStorageUI.transform.Find("Container/ItemsGridList").transform);

    foreach (InventoryItem item in playerStorageItems)
    {
      playerStorageUI.AddItem(item, Resources.Load<InventoryItemPrefab>("Prefabs/Player/Inventory/UI_InventoryItem"), item.currentStackSize, true);
    }

    foreach (var itemPrefab in playerStorageUI.GetComponentsInChildren<InventoryItemPrefab>())
    {
      itemPrefab.OnItemDroppedToInventory += RemoveItem;
    }

    foreach (var slot in playerStorageUI.GetComponentsInChildren<StorageChestSlot>())
    {
      slot.OnItemDropped += AddItem;
    }
  }

  public void AddItem(InventoryItem item)
  {
    // Check if the item is already in the storage
    if (!playerStorageItems.Exists(i => i.GetUniqueID() == item.GetUniqueID()))
    {
      playerStorageItems.Add(item);
    }
  }

  public void RemoveItem(InventoryItem item)
  {
    // Remove the item from the storage based on its unique identifier
    InventoryItem itemToRemove = playerStorageItems.Find(i => i.GetUniqueID() == item.GetUniqueID());
    if (itemToRemove != null)
    {
      playerStorageItems.Remove(itemToRemove);
    }
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      if (playerStorageUI != null)
      {
        foreach (var slot in playerStorageUI.GetComponentsInChildren<StorageChestSlot>())
        {
          slot.OnItemDropped -= AddItem;
        }

        Destroy(playerStorageUI.gameObject);
        playerStorageUI = null;
      }
    }
  }
}
