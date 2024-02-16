# Inventory System

**Setup the inventory system**

- Include the SpellStone Inventory Library.
- Create an inventory UI
  - One InventoryGrid prefab that holds all the inventory slots. Preferably a panel. Add the `InventoryGrid.cs` script to this prefab.
  - One InventorySlot prefab that holds the specific items. Preferably a panel with an image as an child that is disabled. Add the `InventorySlot.cs` script to this prefab and reference the image in the `public Image icon;` variable.
- Create your first InventoryItem in the Projects window. Right Click `"Create > Items > New Item"` to create a new item. Fill in a name and add an item icon.
- Create a new file called `PlayerInventory.cs` and paste the following code..

```cs
using UnityEngine;
using SpellStone.Inventory;

public class PlayerInventory : MonoBehaviour
{
  public InventoryGrid inventoryGridPrefab;
  private InventoryGrid inventoryGrid;

  private void Start()
  {
    CreateInventoryGrid();
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
      InventoryItem item = Resources.FindObjectsOfTypeAll(typeof(InventoryItem))[0] as InventoryItem;
      inventoryGrid.AddItem(item);
  }

  private void CreateInventoryGrid()
  {
      inventoryGrid = Instantiate(inventoryGridPrefab, inventoryGridPrefab.transform.position, Quaternion.identity);

      // Hide the inventory grid by default
      inventoryGrid.gameObject.SetActive(false);
  }
}
```

- DONE
