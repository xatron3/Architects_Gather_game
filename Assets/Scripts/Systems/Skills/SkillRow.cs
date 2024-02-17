using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace SpellStone.Skills
{
  public class SkillRow : MonoBehaviour
  {
    public TMP_Text skillNameText;
    public TMP_Text skillLevelText;
    public ExperienceSliderElements experienceSliderElements;

    public void SetSkillName(string skillName)
    {
      skillNameText.text = skillName;
    }

    public void SetSkillLevelText(int experience)
    {
      skillLevelText.text = experience.ToString();
    }

    public void SetExperienceSlider(int currentExperience, int nextLevelExperience)
    {
      experienceSliderElements.skillExperienceText.text = $"{currentExperience}/{nextLevelExperience}";
      experienceSliderElements.skillExperienceSlider.maxValue = nextLevelExperience;
      experienceSliderElements.skillExperienceSlider.value = currentExperience;
    }

    public string GetSkillName()
    {
      return skillNameText.text;
    }

    [Serializable]
    public class ExperienceSliderElements
    {
      public TMP_Text skillExperienceText;
      public Slider skillExperienceSlider;
    }
  }
}
