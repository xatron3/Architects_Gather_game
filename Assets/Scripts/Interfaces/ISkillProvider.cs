public interface ISkillProvider
{
  SkillBase GetSkill();
  int GetExperienceGain();
  int RequiredLevel { get; }
}
