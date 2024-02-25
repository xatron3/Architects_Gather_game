using System.Collections.Generic;
using UnityEngine;
using SpellStone.Inventory;

public class ItemsManager : MonoBehaviour
{
  private static ItemsManager _instance;
  public static ItemsManager Instance
  {
    get
    {
      if (_instance == null)
      {
        _instance = FindObjectOfType<ItemsManager>();
        if (_instance == null)
        {
          Debug.LogError("ItemsManager instance not found in the scene.");
        }
      }
      return _instance;
    }
  }

  private Dictionary<string, InventoryItem> itemsDictionary = new Dictionary<string, InventoryItem>();

  // Load all items during initialization
  private void Awake()
  {
    if (_instance != null && _instance != this)
    {
      Destroy(gameObject);
      return;
    }
    _instance = this;

    LoadAllItems();
  }

  // Load all items from Resources folder including subfolders
  private void LoadAllItems()
  {
    string[] folders = new string[] { "Items" }; // Add more folders if needed
    foreach (string folder in folders)
    {
      LoadItemsFromFolder(folder);
    }
  }

  // Load items from a specific folder
  private void LoadItemsFromFolder(string folderPath)
  {
    InventoryItem[] items = Resources.LoadAll<InventoryItem>(folderPath);
    foreach (InventoryItem item in items)
    {
      // Use the prefab name as the key in the dictionary
      itemsDictionary[item.itemName] = item;
    }

    // Recursively load items from subfolders
    string[] subFolders = System.IO.Directory.GetDirectories(Application.dataPath + "/Resources/" + folderPath);
    foreach (string subFolder in subFolders)
    {
      string subFolderName = System.IO.Path.GetFileName(subFolder);
      LoadItemsFromFolder(folderPath + "/" + subFolderName);
    }
  }

  // Get an item by its prefab name
  public InventoryItem GetItem(string itemName)
  {
    InventoryItem item;
    if (itemsDictionary.TryGetValue(itemName, out item))
    {
      return item;
    }
    else
    {
      Debug.LogError("Item not found: " + itemName);
      return null;
    }
  }
}
