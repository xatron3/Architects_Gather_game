using UnityEngine;
using TMPro;

namespace SpellStone.Skills
{
  public class SkillRow : MonoBehaviour
  {
    public TMP_Text skillNameText;
    public TMP_Text skillExperienceText;

    public void SetSkillName(string skillName)
    {
      skillNameText.text = skillName;
    }

    public void SetSkillExperience(int experience)
    {
      skillExperienceText.text = experience.ToString();
    }

    public string GetSkillName()
    {
      return skillNameText.text;
    }
  }
}
