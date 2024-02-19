using UnityEngine;
using SpellStone.Messages;

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
      MessagingService.Instance.ShowMessage("You need a stone axe to chop down this tree.", Color.red);
      return;
    }

    if (skill.GetSkill("Woodcutting").GetSkillLevel() < RequiredLevel)
    {
      MessagingService.Instance.ShowMessage($"You need level {RequiredLevel} woodcutting to chop down this tree.", Color.red);
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

  public Vector3 GetPosition()
  {
    return transform.position;
  }

  public int RequiredLevel => 1;

  public bool HasRequiredItem => playerInventory.ContainsItem("Stone Axe");
}