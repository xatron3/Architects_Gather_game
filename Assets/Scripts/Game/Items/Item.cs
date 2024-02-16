using UnityEngine;

public class Item : MonoBehaviour
{
  private Rigidbody rb;

  private void Start()
  {
    TryGetComponent(out rb);
    Debug.Assert(rb != null, "Rigidbody component not found on item");
    Debug.Log("Item initialized");
  }

  private void OnTriggerEnter(Collider other)
  {
    // Check if the item has collided with the terrain
    if (other.CompareTag("Terrain"))
    {
      Debug.Log("Item collided with terrain");
      rb.useGravity = false;
      rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezePositionY;

      // Add Y offset to prevent the item from sinking into the terrain
      float yOffset = transform.position.y + transform.localScale.y / 2;

      transform.position = new Vector3(transform.position.x, yOffset, transform.position.z);
    }
  }

  private void OnTriggerStay(Collider other)
  {
    // Add any continuous interaction logic here if needed
    // For example, you might want to continuously adjust the position of the item
    // based on the terrain's surface to prevent it from sinking into the terrain
  }

  private void OnTriggerExit(Collider other)
  {
    // Re-enable the item's Rigidbody gravity when it exits the terrain
    // if (other.CompareTag("Terrain"))
    // {
    //   rb.useGravity = true;
    // }
  }
}