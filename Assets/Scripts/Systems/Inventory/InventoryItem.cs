using UnityEngine;

namespace SpellStone.Inventory
{
  [CreateAssetMenu(fileName = "New Item", menuName = "Items/New Item")]
  public class InventoryItem : ScriptableObject
  {
    public string itemName = "New Item";
    public Sprite icon = null;

    public virtual void Use()
    {
      // Use the item
      // Something might happen
      Debug.Log("Using " + itemName);
    }
  }
}