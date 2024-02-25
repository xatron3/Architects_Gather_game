public interface ISkill
{
  void SetupSkill(int experience, int skillLevel);
  void PerformSkillAction(int experienceGain, float expGainMultiplier);
  bool HasRequirementsMet();
  int GetSkillLevel();
  string GetName();
  int GetExperience();
}
