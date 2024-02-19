using System.Collections.Generic;
using SpellStone.Inventory;
using UnityEngine;
using System;

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

  public override SerializedPlayerPlacedItems Serialize()
  {
    SerializedPlayerPlacedItems serializedItem = new SerializedPlayerPlacedItems(ItemName, transform.position, transform.rotation);

    // Serialize custom attributes
    if (storageChestItems.Count > 0)
    {
      List<StorageChestItemData> itemsData = new List<StorageChestItemData>();
      foreach (var item in storageChestItems)
      {
        // Serialize only the item name and current stack size
        StorageChestItemData itemData = new StorageChestItemData(item.itemName, item.currentStackSize);
        itemsData.Add(itemData);
      }

      serializedItem.CustomAttributesData.StorageChestContents = itemsData;
    }

    return serializedItem;
  }

  public override PlayerPlacedItem Deserialize(SerializedPlayerPlacedItems serializedItem)
  {
    // Instantiate the item
    StorageChest newItem = Instantiate(this, serializedItem.Position.ToVector3(), serializedItem.Rotation.ToQuaternion());

    // Deserialize custom attributes
    if (serializedItem.CustomAttributesData.StorageChestContents != null)
    {
      List<StorageChestItemData> itemsData = serializedItem.CustomAttributesData.StorageChestContents;
      foreach (var itemData in itemsData)
      {
        InventoryItem item = Resources.Load<InventoryItem>("Items/" + itemData.itemName);
        if (item == null)
        {
          Debug.LogError("Item not found in Resources/Items/" + itemData.itemName);
          continue;
        }

        item = item.GetCopy();
        item.currentStackSize = itemData.currentStackSize;
        newItem.storageChestItems.Add(item);
      }
    }

    // Other custom attributes deserialization can be added similarly
    return newItem;
  }

}

[Serializable]
public class StorageChestItemData
{
  public string itemName;
  public int currentStackSize;

  public StorageChestItemData(string itemName, int currentStackSize)
  {
    this.itemName = itemName;
    this.currentStackSize = currentStackSize;
  }
}

[Serializable]
public class StorageChestItemsData
{
  public List<StorageChestItemData> itemsData;

  public StorageChestItemsData(List<StorageChestItemData> itemsData)
  {
    this.itemsData = itemsData;
  }
}