using UnityEngine;

public class SkillWoodcutting : SkillBase
{
  private int experience;
  private int skillLevel;

  public override void SetupSkill(int experience, int skillLevel)
  {
    this.experience = experience;
    this.skillLevel = skillLevel;
  }

  public override void PerformSkillAction(int experienceGain)
  {
    experience += experienceGain;
  }

  public override bool HasRequirementsMet()
  {
    return true;
  }

  public override int GetExperience()
  {
    return experience;
  }

  public override int GetSkillLevel()
  {
    return skillLevel;
  }

  public override string GetName()
  {
    return "Woodcutting";
  }
}

