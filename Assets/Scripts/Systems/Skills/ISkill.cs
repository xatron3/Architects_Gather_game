public interface ISkill
{
  void SetupSkill(int experience, int skillLevel);
  void PerformSkillAction();
  int CalculateExperienceGain();
  bool HasRequirementsMet();
  int GetSkillLevel();
  string GetName();
  int GetExperience();
}
