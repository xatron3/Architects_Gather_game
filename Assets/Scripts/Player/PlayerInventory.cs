using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
  public InventoryGrid inventoryGridPrefab;
  private InventoryGrid inventoryGrid;

  private void Start()
  {
    GameObject UI_Canvas_GO = GameObject.Find("UI_Canvas");

    if (UI_Canvas_GO != null)
    {
      inventoryGrid = Instantiate(inventoryGridPrefab, inventoryGridPrefab.transform.position, Quaternion.identity);
      inventoryGrid.transform.SetParent(UI_Canvas_GO.transform, false);

      // Hide the inventory grid by default
      inventoryGrid.gameObject.SetActive(false);
    }
    else
      Debug.LogError("UI_Canvas not found");
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.I))
    {
      // For testing purposes, add an item to the player's inventory when pressing "I"
      AddItemToInventory();
    }

    if (Input.GetKeyDown(KeyCode.B))
    {
      // Toggle the inventory grid when pressing "B"
      inventoryGrid.gameObject.SetActive(!inventoryGrid.gameObject.activeSelf);
    }
  }

  private void AddItemToInventory()
  {
    Item resourceItem;
    // Example of adding an item to the player's inventory
    resourceItem = FindAnyObjectByType<Item>();

    if (resourceItem == null)
      Debug.LogError("resourceItem item not found");
    else
    {
      inventoryGrid.AddItem(resourceItem);
      Destroy(resourceItem.gameObject);
      Debug.Log("Added " + resourceItem.itemName + " to inventory");
      Debug.Log("Total items in inventory: " + inventoryGrid.GetTotalItems() + " / Free slots: " + inventoryGrid.GetFreeSlots());
    }
  }
}