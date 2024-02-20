using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
  [HideInInspector] public PlayerScripts playerScripts;

  void Start()
  {
    playerScripts.actionBar = GetComponent<PlayerActionBar>();
    playerScripts.inventory = GetComponent<PlayerInventory>();
  }
}

[Serializable]
public class PlayerScripts
{
  public PlayerActionBar actionBar;
  public PlayerInventory inventory;
}