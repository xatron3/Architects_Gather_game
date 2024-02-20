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

      if (Physics.Raycast(ray, out hit, Mathf.Infinity))
      {
        transform.position = new Vector3(hit.point.x, transform.position.y, hit.point.z);
      }
    }
  }

  public void SetIsPreview(bool isPreview = false)
  {
    IsPreview = isPreview;

    // Set the item to be semi-transparent
    Color itemColor = GetComponent<Renderer>().material.color;
    itemColor.a = isPreview ? 0.5f : 1f; // Set alpha to make it semi-transparent
    GetComponent<Renderer>().material.color = itemColor;

    // Disable the collider
    GetComponent<Collider>().enabled = !isPreview;
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
