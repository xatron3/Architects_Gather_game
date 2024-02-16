using UnityEngine;
using SpellStone.Inventory;

public class PlayerInventory : MonoBehaviour
{
  public InventoryGrid inventoryGridPrefab;
  private InventoryGrid inventoryGrid;

  private void Start()
  {
    CreateInventoryGrid();
    ItemEventManager.Instance.onItemPickedUp.AddListener(OnItemPickedUp);
  }

  private void OnDestroy()
  {
    ItemEventManager.Instance.onItemPickedUp.RemoveListener(OnItemPickedUp);
  }

  private void OnItemPickedUp(InventoryItem item)
  {
    inventoryGrid.AddItem(item);
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
      inventoryGrid.transform.SetParent(UI_Canvas_GO.transform, false);

      // Hide the inventory grid by default
      inventoryGrid.gameObject.SetActive(false);
    }
    else
      Debug.LogError("UI_Canvas not found. Could not create inventory grid.");
  }
}