using UnityEngine;

public interface IInteractable
{
  float InteractionRadius { get; }
  public string Tooltip { get; }

  Vector3 GetPosition();
  void Interact();
  void OnMoveOutOfRange();

  void OnDrawGizmos();
}