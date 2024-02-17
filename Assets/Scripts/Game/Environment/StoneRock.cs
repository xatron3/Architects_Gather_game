using UnityEngine;
using SpellStone.Inventory;

public class StoneRock : MonoBehaviour, IInteractable, ISkillProvider
{
  public ItemPickupable stonePrefab;
  public string Tooltip => "Mine Stone Rock";

  private PlayerInventory playerInventory;

  public void Interact()
  {
    PlayerSkills skill = FindObjectOfType<PlayerSkills>();
    playerInventory = FindObjectOfType<PlayerInventory>();

    if (!HasRequiredItem)
    {
      Debug.Log("You need a stone pickaxe to mine this rock.");
      return;
    }

    if (skill.GetSkill("Mining").GetSkillLevel() < RequiredLevel)
    {
      Debug.Log("You need a higher mining level to mine this rock.");
      return;
    }

    skill.PerformSkillAction(this);

    Destroy(gameObject);

    for (int i = 0; i < 3; i++)
    {
      Vector3 randomPosition = new Vector3(
        Random.Range(-1f, 1f),
        0,
        Random.Range(-1f, 1f)
      );
      Instantiate(stonePrefab, transform.position + randomPosition, Quaternion.identity);
    }
  }

  public SkillBase GetSkill()
  {
    return new SkillMining();
  }

  public int GetExperienceGain()
  {
    return 50;
  }

  public int RequiredLevel => 1;

  public bool HasRequiredItem => playerInventory.ContainsItem("Stone Pickaxe");
}