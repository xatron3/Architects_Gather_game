using System.Collections.Generic;
using SpellStone.Inventory;
using UnityEngine;

public class EnvPlayerStorage : MonoBehaviour, IInteractable
{
  public InventoryGrid playerStorageUIPrefab;
  public StorageInventorySlot playerStorageSlotPrefab;

  private InventoryGrid playerStorageUI;
  public static List<InventoryItem> playerStorageItems = new List<InventoryItem>();

  public string Tooltip => "Open player storage";

  public void Interact()
  {
    Transform UI_Container = GameObject.Find("UI_Elements/Container").transform;

    playerStorageUI = Instantiate(playerStorageUIPrefab, UI_Container, false);

    playerStorageUI.InitializeGrid(20, playerStorageSlotPrefab, playerStorageUI.transform.Find("Container/ItemsGridList").transform);

    foreach (InventoryItem item in playerStorageItems)
    {
      playerStorageUI.AddItem(item, Resources.Load<InventoryItemPrefab>("Prefabs/Player/Inventory/UI_InventoryItem"));
    }

    foreach (var slot in playerStorageUI.GetComponentsInChildren<StorageInventorySlot>())
    {
      slot.OnItemDropped += AddItem;
    }
  }

  public void AddItem(InventoryItem item)
  {
    Debug.Log("Adding item to storage: " + item.uniqueID);
    // Check if the item is already in the storage
    if (!playerStorageItems.Exists(i => i.uniqueID == item.uniqueID))
    {
      playerStorageItems.Add(item);
    }
  }

  public void RemoveItem(InventoryItem item)
  {
    // Remove the item from the storage based on its unique identifier
    InventoryItem itemToRemove = playerStorageItems.Find(i => i.uniqueID == item.uniqueID);
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
        foreach (var slot in playerStorageUI.GetComponentsInChildren<StorageInventorySlot>())
        {
          slot.OnItemDropped -= AddItem;
        }

        Destroy(playerStorageUI.gameObject);
        playerStorageUI = null;
      }
    }
  }
}
