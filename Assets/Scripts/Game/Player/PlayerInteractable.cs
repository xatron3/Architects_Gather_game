using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class PlayerInteractable : MonoBehaviour
{
  public float interactionRadius = 3f; // Interaction range
  public GameObject interactableText;

  private List<IInteractable> interactablesInRange = new List<IInteractable>();

  private void Update()
  {
    // Check for player input
    if (Input.GetKeyDown(KeyCode.E) && interactablesInRange.Count > 0)
    {
      interactablesInRange[0].Interact();
      interactablesInRange.RemoveAt(0);
      Debug.Log("Interacted with object " + interactablesInRange.Count);

      if (interactablesInRange.Count > 0)
        UpdateInteractableText();
      else
        SetCanvasAlpha(0);
    }
  }

  private void OnControllerColliderHit(ControllerColliderHit hit)
  {
    Debug.Log("Hit: " + hit.collider.name);
    IInteractable interactable = hit.collider.GetComponent<IInteractable>();
    if (interactable != null)
    {
      if (!interactablesInRange.Contains(interactable))
        interactablesInRange.Add(interactable);

      UpdateInteractableText();
    }
  }

  private void UpdateInteractableText()
  {
    interactableText.GetComponent<TMP_Text>().text = interactablesInRange[0].Tooltip;
    SetCanvasAlpha(1);
  }

  private void SetCanvasAlpha(float alpha)
  {
    CanvasGroup canvasGroup = interactableText.GetComponent<CanvasGroup>();
    canvasGroup.alpha = alpha;
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.yellow;
    Gizmos.DrawWireSphere(transform.position, interactionRadius);
  }
}
