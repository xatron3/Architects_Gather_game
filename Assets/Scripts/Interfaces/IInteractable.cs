using UnityEngine;

public interface IInteractable
{
  public string Tooltip { get; }

  void Interact();
  void OnMoveOutOfRange();
  Vector3 GetPosition();
}