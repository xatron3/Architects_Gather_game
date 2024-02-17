using UnityEngine;

public class Item : MonoBehaviour
{
  private Rigidbody rb;

  private void Start()
  {
    TryGetComponent(out rb);
    Debug.Assert(rb != null, "Rigidbody component not found on item");
  }

  private void OnTriggerEnter(Collider other)
  {
    if (other.CompareTag("Terrain"))
    {
      rb.useGravity = false;
      rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;

      float yOffset = transform.position.y + transform.localScale.y / 2;

      transform.position = new Vector3(transform.position.x, yOffset, transform.position.z);
    }
  }
}