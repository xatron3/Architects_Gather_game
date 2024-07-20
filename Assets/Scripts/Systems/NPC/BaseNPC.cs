using UnityEngine;
public class BaseNPC : MonoBehaviour, INpc, IInteractable
{
  public string Name { get; set; }

  public float InteractionRadius => 3f;

  public string Tooltip => $"Talk to {Name}";

  public void Interact(GameObject interactor)
  {
    Debug.Log($"Interacting with {Name}");
  }

  public Vector3 GetPosition()
  {
    return transform.position;
  }

  public void OnMoveOutOfRange()
  {
    Debug.Log("Player moved out of range");
  }

  public void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawWireSphere(transform.position, InteractionRadius);
  }
}