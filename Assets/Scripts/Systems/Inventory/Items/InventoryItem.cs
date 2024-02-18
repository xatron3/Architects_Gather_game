using UnityEngine;

namespace SpellStone.Inventory
{
  [CreateAssetMenu(fileName = "New Item", menuName = "Items/New Item")]
  public class InventoryItem : ScriptableObject
  {
    public string itemName = "New Item";
    public Sprite icon = null;
    public bool canEquip = false;

    public virtual void Use()
    {
      Debug.Log("Using " + itemName);
    }
  }
}