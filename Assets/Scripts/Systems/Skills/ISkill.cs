public interface ISkill
{
  void PerformSkillAction();
  int CalculateExperienceGain();
  bool HasRequirementsMet();
  int GetSkillLevel();
  string GetName();
  int GetExperience();
}
