using SpellStone.Inventory;

public interface ISkillProvider
{
  SkillBase GetSkill();
  int GetExperienceGain();
  int RequiredLevel { get; }
  ToolItem RequiredItem { get; }
}
