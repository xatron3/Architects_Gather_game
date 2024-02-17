using UnityEngine;
using SpellStone.ActionBar;
using SpellStone.Inventory;

public class PlayerActionBar : MonoBehaviour
{
  public ActionBarGrid actionBarGrid;
  private ActionBarGrid playerActionBarGrid;

  private InventoryItemPrefab itemIconPrefab;

  void Start()
  {
    GameObject UI_Canvas_GO = GetComponentInChildren<Canvas>().gameObject;

    if (UI_Canvas_GO != null)
    {
      playerActionBarGrid = Instantiate(actionBarGrid, transform);
      playerActionBarGrid.transform.SetParent(UI_Canvas_GO.transform.Find("Container").transform, false);

      itemIconPrefab = Resources.Load<InventoryItemPrefab>("Prefabs/Player/Inventory/UI_InventoryItemPrefab");

      playerActionBarGrid.gameObject.SetActive(true);
    }
  }

  void Update()
  {
    // Handle keyboard input for item equipping
    for (int i = 0; i < playerActionBarGrid.slots.Count; i++)
    {
      if (Input.GetKeyDown(KeyCode.Alpha1 + i)) // Assuming keys 1-8
      {
        EquipItem(i);
      }
    }
  }

  public void AddItem(InventoryItem item)
  {
    playerActionBarGrid.AddItem(item, itemIconPrefab);
  }

  private void EquipItem(int slotIndex)
  {
    ActionBarSlot slot = playerActionBarGrid.slots[slotIndex];
    InventoryItemPrefab itemPrefab = slot.GetComponentInChildren<InventoryItemPrefab>();

    // Check if slot has an item
    if (itemPrefab != null)
    {
      // Use the item
      itemPrefab.UseItem();
    }
  }
}
