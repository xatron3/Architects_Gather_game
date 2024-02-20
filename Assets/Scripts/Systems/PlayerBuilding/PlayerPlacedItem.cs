using UnityEngine;

public class PlayerPlacedItem : MonoBehaviour
{
  public virtual string ItemName { get; set; }

  public bool IsPreview { get; set; }
  private PlayerController player;

  void Update()
  {
    if (player == null)
    {
      player = FindObjectOfType<PlayerController>();
      return;
    }

    if (IsPreview && player != null)
    {
      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

      // Get rotation from the PlayerPlacedItems singleton
      Quaternion rotation = Quaternion.Euler(0, (int)PlayerPlacedItems.Instance.placingSettings.rotation * 90, 0);

      if (Physics.Raycast(ray, out hit, Mathf.Infinity))
      {
        transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        transform.rotation = rotation;
      }
    }
  }

  public void SetIsPreview(bool isPreview = false)
  {
    IsPreview = isPreview;
    PlayerPlacedItems.Instance.IsPlacingItem = isPreview;
    GetComponent<Collider>().enabled = !isPreview;

    // Check if there is childs in the object and disable / enable their colliders
    foreach (Transform child in transform)
    {
      child.GetComponent<Collider>().enabled = !isPreview;
    }
  }

  public virtual SerializedPlayerPlacedItems Serialize()
  {
    SerializedPlayerPlacedItems serializedItem = new SerializedPlayerPlacedItems(ItemName, transform.position, transform.rotation);

    return serializedItem;
  }

  public virtual PlayerPlacedItem Deserialize(SerializedPlayerPlacedItems serializedItem)
  {
    // Instantiate the item
    PlayerPlacedItem newItem = Instantiate(this, serializedItem.Position.ToVector3(), serializedItem.Rotation.ToQuaternion());

    return newItem;
  }
}
