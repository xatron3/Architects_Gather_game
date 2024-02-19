using UnityEngine;
using SpellStone.Skills;
using System.Collections.Generic;

public class PlayerSkills : MonoBehaviour, IPlayerSkills
{
  public SkillsContainer skillContainerPrefab;
  private SkillsContainer skillContainer;

  private List<SkillBase> skills = new List<SkillBase>();

  private void Start()
  {
    CreateSkillContainer();
    InitializeSkills();
  }

  // Save skills when the game is closed
  private void OnApplicationQuit()
  {
    SaveSkills();
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.K))
    {
      skillContainer.gameObject.SetActive(!skillContainer.gameObject.activeSelf);
    }
  }

  public void PerformSkillAction(ISkillProvider skillProvider)
  {
    SkillBase skill = skillProvider.GetSkill();
    int experienceGain = skillProvider.GetExperienceGain();

    // Perform the skill action on my skill
    SkillBase mySkill = skills.Find(s => s.GetName() == skill.GetName());
    if (mySkill == null)
    {
      Debug.LogError("Skill not found");
      return;
    }

    mySkill.PerformSkillAction(experienceGain);
    skillContainer.UpdateSkillRow(mySkill);
  }

  public SkillBase GetSkill(string skillName)
  {
    return skills.Find(s => s.GetName() == skillName);
  }

  public void AddExperienceToSkill(string skillName, int experience)
  {
    SkillBase skill = skills.Find(s => s.GetName() == skillName);
    if (skill != null)
    {
      skill.AddExperience(experience);
      skillContainer.UpdateSkillRow(skill);
    }
  }

  private void CreateSkillContainer()
  {
    GameObject UI_Canvas_GO = GetComponentInChildren<Canvas>().gameObject;

    if (UI_Canvas_GO != null)
    {
      skillContainer = Instantiate(skillContainerPrefab, skillContainerPrefab.transform.position, Quaternion.identity);
      skillContainer.transform.SetParent(UI_Canvas_GO.transform.Find("Container").transform, false);
      skillContainer.gameObject.SetActive(false);
    }
    else
      Debug.LogError("UI_Canvas not found. Could not create skill container.");
  }

  private void InitializeSkills()
  {
    List<SkillBase> loadedSkills = SaveLoadManager.LoadPlayerSkills();

    if (loadedSkills != null)
    {
      skills = loadedSkills;
      skillContainer.CreateSkillRows(skills);
      return;
    }
    else
    {
      Debug.Log("No skills found. Creating new skills.");
    }

    // Instantiate WoodcuttingSkill
    SkillWoodcutting woodcuttingSkill = new SkillWoodcutting();
    woodcuttingSkill.SetupSkill(0, 1);
    skills.Add(woodcuttingSkill);

    SkillMining miningSkill = new SkillMining();
    miningSkill.SetupSkill(0, 1);
    skills.Add(miningSkill);

    SkillCrafting craftingSkill = new SkillCrafting();
    craftingSkill.SetupSkill(0, 1);
    skills.Add(craftingSkill);

    // Populate UI with WoodcuttingSkill
    skillContainer.CreateSkillRows(skills);
  }

  public void SaveSkills()
  {
    SaveLoadManager.SavePlayerSkills(skills);
  }
}
