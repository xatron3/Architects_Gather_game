public class SkillMining : SkillBase
{
  private int experience;
  private int skillLevel;

  public override void SetupSkill(int experience, int skillLevel)
  {
    this.experience = experience;
    this.skillLevel = skillLevel;
  }

  public override void PerformSkillAction()
  {
    this.experience += CalculateExperienceGain();
  }

  public override int CalculateExperienceGain()
  {
    return 10;
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
