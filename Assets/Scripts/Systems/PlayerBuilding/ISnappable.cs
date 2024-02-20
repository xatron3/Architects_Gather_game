using UnityEngine;

public interface ISnappable
{
  bool IsSnappable { get; }
  Transform Transform { get; }
  void SnapTo(ISnappable snappableItem);
}
