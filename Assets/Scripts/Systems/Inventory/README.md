# Inventory System

**Setup the inventory system**

- Include the SpellStone Inventory Library.
- Create an inventory UI
  - One InventoryGrid prefab that holds all the inventory slots. Preferably a panel. Add the `InventoryGrid.cs` script to this prefab.
  - One InventorySlot prefab that holds the specific items. Preferably a panel with an image as an child that is disabled. Add the `InventorySlot.cs` script to this prefab and reference the image in the `public Image icon;` variable.
- Create your first InventoryItem in the Projects window. Right Click `"Create > Items > New Item"` to create a new item. Fill in a name and add an item icon.
