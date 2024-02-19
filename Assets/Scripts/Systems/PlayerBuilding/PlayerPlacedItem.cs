using UnityEngine;

public class PlayerPlacedItem : MonoBehaviour
{
  public virtual string ItemName { get; set; }

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