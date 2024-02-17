using UnityEngine;

public class SmallTree : MonoBehaviour, IInteractable, ISkillProvider
{
  public ItemPickupable logPrefab;
  public string Tooltip => "Chop down tree";

  private PlayerInventory playerInventory;

  public void Interact()
  {
    PlayerSkills skill = FindObjectOfType<PlayerSkills>();
    playerInventory = FindObjectOfType<PlayerInventory>();

    if (!HasRequiredItem)
    {
      Debug.Log("You need a stone axe to chop down this tree.");
      return;
    }

    if (skill.GetSkill("Woodcutting").GetSkillLevel() < RequiredLevel)
    {
      Debug.Log("You need a higher woodcutting level to chop down this tree.");
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
      Instantiate(logPrefab, transform.position + randomPosition, Quaternion.identity);
    }
  }

  public SkillBase GetSkill()
  {
    return new SkillWoodcutting();
  }

  public int GetExperienceGain()
  {
    return 10;
  }

  public int RequiredLevel => 1;

  public bool HasRequiredItem => playerInventory.ContainsItem("Stone Axe");
}