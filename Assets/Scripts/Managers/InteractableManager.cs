using UnityEngine;

public class InteractableManager : MonoBehaviour
{
  public float interactionRadius = 3f; // Interaction range

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
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, interactionRadius);
  }
}
