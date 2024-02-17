public abstract class SkillBase : ISkill
{
  public abstract void SetupSkill(int experience, int skillLevel);
  public abstract void PerformSkillAction(int experienceGain);
  public abstract bool HasRequirementsMet();
  public abstract string GetName();
  public abstract int GetExperience();
  public abstract int GetSkillLevel();
}
