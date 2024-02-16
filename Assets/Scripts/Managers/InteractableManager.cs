using UnityEngine;

public class InteractableManager : MonoBehaviour
{
  public float interactionRadius = 3f; // Interaction range
  public GameObject interactableText;

  private void Update()
  {
    // Check for player input
    if (Input.GetKeyDown(KeyCode.E))
    {
      Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRadius);
      foreach (Collider collider in hitColliders)
      {
        IInteractable interactable = collider.GetComponent<IInteractable>();
        if (interactable != null)
        {
          interactable.Interact();
        }
      }
    }

    CheckForInteractableItem();
  }

  private void CheckForInteractableItem()
  {
    Collider[] hitColliders = Physics.OverlapSphere(transform.position, interactionRadius);
    bool interactableFound = false;

    foreach (Collider collider in hitColliders)
    {
      IInteractable interactable = collider.GetComponent<IInteractable>();
      if (interactable != null)
      {
        interactableFound = true;
        interactableText.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = interactable.Tooltip;
        break;
      }
    }

    interactableText.SetActive(interactableFound);
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, interactionRadius);
  }
}
