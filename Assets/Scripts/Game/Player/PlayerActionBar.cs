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

      foreach (ActionBarSlot slot in playerActionBarGrid.slots)
      {
        slot.SetSlotText((playerActionBarGrid.slots.IndexOf(slot) + 1).ToString());
      }

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

  public bool AddItem(InventoryItem item)
  {
    if (playerActionBarGrid.AddItem(item, itemIconPrefab))
    {
      return true;
    }

    return false;
  }

  private void EquipItem(int slotIndex)
  {
    ActionBarSlot slot = playerActionBarGrid.slots[slotIndex];
    InventoryItemPrefab itemPrefab = slot.GetComponentInChildren<InventoryItemPrefab>();

    if (itemPrefab != null)
    {
      // Use the item
      itemPrefab.UseItem();
    }
  }
}
