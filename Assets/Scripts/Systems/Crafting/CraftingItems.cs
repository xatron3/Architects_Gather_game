using UnityEngine;
using SpellStone.Inventory;
using System.Collections.Generic;

namespace SpellStone.Crafting
{
  [CreateAssetMenu(fileName = "New Crafting Item", menuName = "Crafting/New Crafting Item")]
  public class CraftingItem : ScriptableObject
  {
    public string craftingName = "New Item";
    public InventoryItem craftableItem;
    public List<InventoryItem> ingredients;

    public int craftingExperienceGain;
    public int craftingSkillLevelRequirement;
  }
}