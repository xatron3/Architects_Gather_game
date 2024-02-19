using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SpellStone.Inventory;
using System;

public class SaveLoadManager
{
  public static void SavePlayerSkills(List<SkillBase> skills)
  {
    BinaryFormatter formatter = new BinaryFormatter();
    string path = Application.persistentDataPath + "/playerSkills.data";
    FileStream stream = new FileStream(path, FileMode.Create);

    formatter.Serialize(stream, skills);
    stream.Close();
  }

  public static List<SkillBase> LoadPlayerSkills()
  {
    string path = Application.persistentDataPath + "/playerSkills.data";
    if (File.Exists(path))
    {
      BinaryFormatter formatter = new BinaryFormatter();
      FileStream stream = new FileStream(path, FileMode.Open);

      List<SkillBase> skills = formatter.Deserialize(stream) as List<SkillBase>;
      stream.Close();
      return skills;
    }
    else
    {
      Debug.LogError("Save file not found in " + path);
      return null;
    }
  }

  public static void SavePlayerInventory(List<InventoryItem> items)
  {
    BinaryFormatter formatter = new BinaryFormatter();
    string path = Application.persistentDataPath + "/playerInventory.data";
    FileStream stream = new FileStream(path, FileMode.Create);

    List<SerializableInventoryItem> serializableItems = new List<SerializableInventoryItem>();
    foreach (var item in items)
    {
      serializableItems.Add(new SerializableInventoryItem(item.name, item.currentStackSize, item.GetType().ToString()));
    }

    formatter.Serialize(stream, serializableItems);
    stream.Close();
  }

  public static List<InventoryItem> LoadPlayerInventory()
  {
    string path = Application.persistentDataPath + "/playerInventory.data";
    if (File.Exists(path))
    {
      BinaryFormatter formatter = new BinaryFormatter();
      FileStream stream = new FileStream(path, FileMode.Open);

      List<SerializableInventoryItem> serializableItems = formatter.Deserialize(stream) as List<SerializableInventoryItem>;
      stream.Close();

      List<InventoryItem> items = new List<InventoryItem>();
      foreach (var serializableItem in serializableItems)
      {
        Type itemType = Type.GetType(serializableItem.ItemType);

        // Find the item prefab in the resources folder
        InventoryItem item = Resources.Load<InventoryItem>("Items/" + serializableItem.PrefabName);
        item.currentStackSize = serializableItem.CurrentStackSize;
        items.Add(item);
      }

      return items;
    }
    else
    {
      Debug.LogError("Save file not found in " + path);
      return null;
    }
  }

  [Serializable]
  class SerializableInventoryItem
  {
    public string PrefabName;
    public int CurrentStackSize;
    public string ItemType;

    public SerializableInventoryItem(string prefabName, int currentStackSize, string itemType)
    {
      PrefabName = prefabName;
      CurrentStackSize = currentStackSize;
      ItemType = itemType;
    }
  }
}
