using UnityEngine;

namespace SpellStone.Camera
{
  public class CameraFollow : MonoBehaviour
  {
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset = new Vector3(0, 7, -10);
    [SerializeField] private float smoothSpeed = 0.125f;

    private void LateUpdate()
    {
      if (target == null)
      {
        Debug.LogWarning("No target assigned to CameraFollow script.");
        return;
      }

      Vector3 desiredPosition = target.position + offset;
      Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
      transform.position = smoothedPosition;

      transform.LookAt(target.position);
    }

    public void SetTarget(Transform newTarget)
    {
      target = newTarget;
    }
  }
}