using SpellStone.Crafting;
using UnityEngine;
using System.Collections.Generic;

public class CraftingTable : MonoBehaviour, IInteractable
{
  public float InteractionRadius => 4f;
  public List<CraftingItem> craftingItems = new List<CraftingItem>();
  public CraftingUI craftingTableUIPrefab;
  private CraftingUI craftingTableUI;

  private PlayerController player;

  public string Tooltip => "Open crafting table";

  public void Interact(GameObject interactor)
  {
    interactor.TryGetComponent<PlayerController>(out PlayerController _playerController);
    if (_playerController == null)
    {
      Debug.LogError("PlayerController not found on interactor");
      return;
    }

    player = _playerController;

    if (craftingTableUI != null)
      return;

    Transform UI_Container = GameObject.Find("UI_Elements/Container").transform;
    craftingTableUI = Instantiate(craftingTableUIPrefab, UI_Container, false);

    craftingTableUI.SetupUI(craftingItems);

    // Find the crafting preview UI and set the player skills and inventory
    CraftingPreviewUI craftingPreviewUI = craftingTableUI.GetComponentInChildren<CraftingPreviewUI>();
    craftingPreviewUI.SetPlayerSkills(player.scripts.skills);
    craftingPreviewUI.SetPlayerInventory(player.scripts.inventory);
    craftingPreviewUI.SetupUI();
    craftingPreviewUI.AddEventListners();
  }

  public Vector3 GetPosition()
  {
    return transform.position;
  }

  public void OnMoveOutOfRange()
  {
    if (craftingTableUI == null)
      return;

    Destroy(craftingTableUI.gameObject);
    craftingTableUI = null;
    player = null;
  }

  public void OnDrawGizmos()
  {
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, InteractionRadius);
  }
}