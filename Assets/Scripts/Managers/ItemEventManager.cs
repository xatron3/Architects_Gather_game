using UnityEngine;
using UnityEngine.Events;

public class ItemEventManager : MonoBehaviour
{
  public static ItemEventManager Instance;

  // Event to notify when an item is picked up
  public ItemPickedUpEvent onItemPickedUp = new ItemPickedUpEvent();

  private void Awake()
  {
    Instance = this;
  }
}
