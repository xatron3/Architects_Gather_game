using UnityEngine;
using SpellStone.Skills;
using System.Collections.Generic;

public class PlayerSkills : MonoBehaviour
{
  public SkillsContainer skillContainerPrefab;
  private SkillsContainer skillContainer;

  private List<SkillBase> skills = new List<SkillBase>();

  private void Start()
  {
    CreateSkillContainer();
    InitializeSkills();
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.K))
    {
      skillContainer.gameObject.SetActive(!skillContainer.gameObject.activeSelf);
    }

    if (Input.GetMouseButtonDown(0))
    {
      PerformSkillAction();
    }
  }

  private void PerformSkillAction()
  {
    foreach (var skill in skills)
    {
      skill.PerformSkillAction();
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
    // Instantiate WoodcuttingSkill
    SkillWoodcutting woodcuttingSkill = new SkillWoodcutting(0, 1);
    skills.Add(woodcuttingSkill);

    SkillMining miningSkill = new SkillMining(0, 1);
    skills.Add(miningSkill);

    // Populate UI with WoodcuttingSkill
    skillContainer.CreateSkillRows(skills);
  }
}
