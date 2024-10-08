using UnityEngine;
using System;

[Serializable]
public class SkillMining : SkillBase
{
  public override void SetupSkill(int experience, int skillLevel)
  {
    this.experience = experience;
    this.skillLevel = skillLevel;
  }

  public override void PerformSkillAction(int experienceGain, float expGainMultiplier)
  {
    base.PerformSkillAction(experienceGain, expGainMultiplier);
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
    return "Mining";
  }
}
