using UnityEngine;
using SpellStone.Skills;

public class PlayerSkills : MonoBehaviour
{
  public SkillsContainer skillContainerPrefab;
  private SkillsContainer skillContainer;

  private void Start()
  {
    CreateSkillContainer();
  }

  private void Update()
  {
    if (Input.GetKeyDown(KeyCode.K))
    {
      skillContainer.gameObject.SetActive(!skillContainer.gameObject.activeSelf);
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
}