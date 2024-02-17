using System.Collections.Generic;
using UnityEngine;

namespace SpellStone.Skills
{
  public class SkillsContainer : MonoBehaviour
  {
    public SkillRow skillRowPrefab;
    public Transform skillRowsListParent;

    private List<SkillRow> skillRows = new List<SkillRow>();

    public void CreateSkillRows(List<SkillBase> skills)
    {
      foreach (var skill in skills)
      {
        CreateSkillRow(skill);
      }
    }

    public void UpdateSkillRow(SkillBase skill)
    {
      foreach (var row in skillRows)
      {
        if (row.GetSkillName() == skill.GetName())
        {
          row.SetSkillExperience(skill.GetExperience());
          break;
        }
      }
    }

    private void CreateSkillRow(SkillBase skill)
    {
      SkillRow skillRow = Instantiate(skillRowPrefab, skillRowPrefab.transform.position, Quaternion.identity);
      skillRow.transform.SetParent(skillRowsListParent, false);
      skillRow.SetSkillName(skill.GetName());
      skillRow.SetSkillExperience(skill.GetExperience());
      skillRows.Add(skillRow);
    }
  }
}
