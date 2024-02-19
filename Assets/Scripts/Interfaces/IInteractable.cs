using UnityEngine;

public interface IInteractable
{
  public string Tooltip { get; }

  void Interact();
  Vector3 GetPosition();
}