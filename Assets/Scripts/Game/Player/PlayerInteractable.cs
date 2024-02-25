using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class PlayerInteractable : MonoBehaviour, IInteractor
{
  public float interactionRadius = 3f; // Interaction range
  public GameObject interactableText;
  public LayerMask interactableLayer; // Define the layer where your interactable objects reside

  private List<IInteractable> interactablesInRange = new List<IInteractable>();

  private void Update()
  {
    // Check for player input
    if (Input.GetKeyDown(KeyCode.E) && interactablesInRange.Count > 0)
    {
      InteractWith(interactablesInRange[0]);
      interactablesInRange.RemoveAt(0);

      if (interactablesInRange.Count > 0)
        UpdateInteractableText();
      else
        SetCanvasAlpha(0);
    }
  }

  private void FixedUpdate()
  {
    CheckInteractableObjects();
  }

  private void CheckInteractableObjects()
  {
    Collider[] colliders = Physics.OverlapSphere(transform.position, Mathf.Infinity, interactableLayer);

    foreach (Collider collider in colliders)
    {
      IInteractable interactable = collider.GetComponent<IInteractable>();
      if (interactable != null && !interactablesInRange.Contains(interactable))
      {
        float distance = Vector3.Distance(transform.position, interactable.GetPosition());
        if (distance <= interactable.InteractionRadius)
        {
          interactablesInRange.Add(interactable);
          UpdateInteractableText();
        }
      }
    }

    for (int i = interactablesInRange.Count - 1; i >= 0; i--)
    {
      float distance = Vector3.Distance(transform.position, interactablesInRange[i].GetPosition());
      if (distance > interactablesInRange[i].InteractionRadius)
      {
        interactablesInRange[i].OnMoveOutOfRange();
        interactablesInRange.RemoveAt(i);
        if (interactablesInRange.Count == 0)
          SetCanvasAlpha(0);
      }
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

  public void InteractWith(IInteractable interactable)
  {
    interactable.Interact(this.gameObject);
  }
}
