using UnityEngine;
using SpellStone.Messages;
using System;

[Serializable]
public abstract class SkillBase : ISkill
{
  public int experience
  {
    get; set;
  }

  public int skillLevel
  {
    get; set;
  }

  public abstract void SetupSkill(int experience, int skillLevel);

  public virtual void PerformSkillAction(int experienceGain, float expGainMultiplier)
  {
    IncreaseExperience(experienceGain, expGainMultiplier);

    if (experience >= GetNextLevelExperience())
    {
      experience -= GetNextLevelExperience();
      skillLevel++;
      MessagingService.Instance.ShowMessage(GetName() + " leveled up to " + GetSkillLevel(), Color.yellow);
    }
  }

  public void AddExperience(int experience)
  {
    this.experience += experience;
    MessagingService.Instance.ShowMessage("+" + experience + " " + GetName() + " experience", Color.magenta);
  }

  public void IncreaseExperience(int baseExperience, float expGainMultiplier)
  {
    int experienceGain = Mathf.RoundToInt(baseExperience * (1 + (expGainMultiplier / 100)));
    int adjustedExperienceGain = Mathf.RoundToInt(experienceGain * (1 + (GetSkillLevel() * 0.1f)));

    AddExperience(experienceGain);
  }

  public abstract bool HasRequirementsMet();
  public abstract string GetName();
  public abstract int GetExperience();
  public int GetNextLevelExperience()
  {
    return 100 * GetSkillLevel();
  }

  public abstract int GetSkillLevel();
}
