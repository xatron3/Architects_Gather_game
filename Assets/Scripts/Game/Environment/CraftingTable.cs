using UnityEngine;

public class CraftingTable : MonoBehaviour, IInteractable
{
  public GameObject craftingTableUIPrefab;
  private GameObject craftingTableUI;

  public string Tooltip => "Open crafting table";
  public void Interact()
  {
    Transform UI_Container = GameObject.Find("UI_Elements/Container").transform;
    craftingTableUI = Instantiate(craftingTableUIPrefab, UI_Container, false);
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      Destroy(craftingTableUI);
    }
  }
}