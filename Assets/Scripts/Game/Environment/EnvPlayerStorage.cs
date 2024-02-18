using UnityEngine;

public class EnvPlayerStorage : MonoBehaviour, IInteractable
{
  public GameObject playerStorageUIPrefab;
  private GameObject playerStorageUI;

  public string Tooltip => "Open player storage";
  public void Interact()
  {
    Transform UI_Container = GameObject.Find("UI_Elements/Container").transform;

    playerStorageUI = Instantiate(playerStorageUIPrefab, UI_Container, false);
  }

  private void OnTriggerExit(Collider other)
  {
    if (other.CompareTag("Player"))
    {
      Destroy(playerStorageUI);
    }
  }
}