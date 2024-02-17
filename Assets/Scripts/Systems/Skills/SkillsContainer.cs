using System.Collections.Generic;
using UnityEngine;

namespace SpellStone.Skills
{
  public class SkillsContainer : MonoBehaviour
  {
    public SkillRow skillRowPrefab;
    public Transform skillRowsListParent;

    private List<SkillRow> skillRows;

    private void Start()
    {
      skillRows = new List<SkillRow>();
      CreateSkillRows();
    }

    private void CreateSkillRows()
    {
      for (int i = 0; i < 5; i++)
      {
        SkillRow skillRow = Instantiate(skillRowPrefab, skillRowPrefab.transform.position, Quaternion.identity);
        skillRow.transform.SetParent(skillRowsListParent, false);
        skillRow.SetSkillName("Skill " + i);
        skillRow.SetSkillExperience(i * 100);
        skillRows.Add(skillRow);
      }
    }
  }
}