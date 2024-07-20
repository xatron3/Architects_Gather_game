using UnityEngine;

public class PlayerPlacedItem : MonoBehaviour, ISnappable
{
  public virtual string ItemName { get; set; }

  public virtual bool IsSnappable => false;

  public float snappingRadius = 1f;

  public Transform Transform => transform;

  public bool IsPreview { get; set; }
  private PlayerController _player;

  void Update()
  {
    if (_player == null)
    {
      _player = FindObjectOfType<PlayerController>();
      return;
    }

    if (IsPreview && _player != null)
    {
      RaycastHit hit;
      Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

      // Get rotation from the PlayerPlacedItems singleton
      Quaternion rotation = Quaternion.Euler(transform.eulerAngles.x, (int)PlayerPlacedItems.Instance.placingSettings.rotation * 90, transform.eulerAngles.z);

      if (Physics.Raycast(ray, out hit, Mathf.Infinity))
      {
        transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
        transform.rotation = rotation;

        if (PlayerPlacedItems.Instance.placingSettings.isSnapping)
          SnapToClosestSnappableItem(hit.transform);
      }
    }
  }

  protected virtual void SnapToClosestSnappableItem(Transform mouseHitTransform)
  {
    // Find all snappable items within a certain radius
    Collider[] colliders = Physics.OverlapSphere(transform.position, snappingRadius);

    // Iterate through all colliders to find snappable items
    foreach (Collider collider in colliders)
    {
      ISnappable snappableItem = collider.GetComponent<ISnappable>();

      // Ensure the collider has an ISnappable component and is not the current item
      if (snappableItem != null && (object)snappableItem != this)
      {
        // Check if the snappable item is within snapping range and should be snapped to
        if (Vector3.Distance(transform.position, snappableItem.Transform.position) <= snappingRadius)
        {
          SnapTo(snappableItem);
          return; // Snap to the first snappable item found
        }
      }
    }
  }

  public virtual void SnapTo(ISnappable snappableItem)
  {
    // Implement snapping logic here
    if (snappableItem.IsSnappable && PlayerPlacedItems.Instance.placingSettings.isSnapping)
    {
      // Get the transforms of both the current item and the snappable item
      Transform currentTransform = transform;
      Transform snappableTransform = snappableItem.Transform;

      // Calculate the position where the snapping should occur
      // For example, if the current item is a wall, and the snappable item is also a wall,
      // you might want to snap to the closest side of the snappable item
      // You need to adjust this logic based on your game's requirements
      Vector3 snapPosition = CalculateSnapPosition(currentTransform, snappableTransform);

      // Set the position of the current item to the calculated snap position
      transform.position = snapPosition;
    }
  }

  private Vector3 CalculateSnapPosition(Transform currentItemTransform, Transform snappableItemTransform)
  {
    // Get the dimensions of the snappable item (assuming it's a wall)
    Vector3 snappableItemSize = snappableItemTransform.localScale;

    // Calculate the position where snapping should occur
    Vector3 snapPosition = snappableItemTransform.position;

    // Calculate the rotation of the snappable item in degrees
    float snappableItemRotation = snappableItemTransform.eulerAngles.y;

    // Determine which axis to offset based on the rotation angle
    float xOffset = 0f;
    float zOffset = 0f;
    if (Mathf.Approximately(snappableItemRotation, 0f) || Mathf.Approximately(snappableItemRotation, 180f))
    {
      zOffset = Mathf.Sign(snappableItemRotation) * (snappableItemSize.z + currentItemTransform.localScale.z) / 2;
    }
    else if (Mathf.Approximately(snappableItemRotation, 90f) || Mathf.Approximately(snappableItemRotation, -90f))
    {
      xOffset = Mathf.Sign(snappableItemRotation) * (snappableItemSize.x + currentItemTransform.localScale.x) / 2;
    }

    // Adjust the snap position with the calculated offset
    snapPosition += new Vector3(xOffset, 0f, zOffset);

    return snapPosition;
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
