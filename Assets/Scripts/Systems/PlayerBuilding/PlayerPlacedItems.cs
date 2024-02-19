using UnityEngine;
using System.Collections.Generic;

public class PlayerPlacedItems : MonoBehaviour
{
  public static PlayerPlacedItems Instance; // Singleton instance

  public Transform itemsParent; // Parent object to hold all items
  public List<PlayerPlacedItem> items = new List<PlayerPlacedItem>(); // List of items

  private void Awake()
  {
    if (Instance == null)
    {
      Instance = this;
    }
    else
    {
      Destroy(gameObject);
    }
  }

  private void Start()
  {
    LoadItems();
  }

  private void OnApplicationQuit()
  {
    SaveItems();
  }

  // Save items
  public void SaveItems()
  {
    SaveLoadManager.SavePlayerPlacedItems(items);
  }

  // Load items
  public void LoadItems()
  {
    // Load saved item data
    List<PlayerPlacedItem> savedData = SaveLoadManager.LoadPlayerPlacedItems();

    if (savedData == null)
    {
      Debug.Log("No saved items to load");
      return;
    }

    ClearItems();
    // Instantiate items from saved data
    foreach (var savedItemData in savedData)
    {
      savedItemData.transform.SetParent(itemsParent);
      items.Add(savedItemData);
    }
  }

  // Clear items
  private void ClearItems()
  {
    foreach (var item in items)
    {
      Destroy(item);
    }
    items.Clear();
  }
}
