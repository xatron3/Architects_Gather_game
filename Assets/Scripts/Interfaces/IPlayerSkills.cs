public interface IPlayerSkills
{
  SkillBase GetSkill(string skillName);
  void PerformSkillAction(ISkillProvider skillProvider);
}