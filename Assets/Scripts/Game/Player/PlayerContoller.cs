using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [HideInInspector] public PlayerScripts scripts;

  void Start()
  {
    scripts.actionBar = GetComponent<PlayerActionBar>();
    scripts.inventory = GetComponent<PlayerInventory>();
    scripts.skills = GetComponent<PlayerSkills>();
  }
}

[Serializable]
public class PlayerScripts
{
  public PlayerActionBar actionBar;
  public PlayerInventory inventory;
  public PlayerSkills skills;
}