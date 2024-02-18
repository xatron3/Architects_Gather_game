using System.Collections.Generic;
using SpellStone.Inventory;
using UnityEngine;

public class EnvPlayerStorage : MonoBehaviour, IInteractable
{
  public InventoryGrid playerStorageUIPrefab;
  public StorageInventorySlot playerStorageSlotPrefab; // Change to StorageInventorySlot

  private InventoryGrid playerStorageUI;
  public static List<InventoryItem> playerStorageItems = new List<InventoryItem>(); // Static list

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
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      if (playerStorageUI != null)
      {
        Destroy(playerStorageUI.gameObject);
        playerStorageUI = null;
      }
    }
  }
}

