using UnityEngine;
using TMPro;
using System;

namespace SpellStone.Skills
{
  public class SkillRow : MonoBehaviour
  {
    public SkillRowElements elements;

    public void SetSkillName(string skillName)
    {
      elements.skillName.text = skillName;
    }

    public void SetSkillExperience(int experience)
    {
      elements.skillExperience.text = experience.ToString();
    }
  }

  [Serializable]
  public class SkillRowElements
  {
    public TMP_Text skillName;
    public TMP_Text skillExperience;
  }
}