using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SpellStone.Inventory;
using System;

public class SaveLoadManager
{
  #region Player Skills
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
  #endregion

  #region Player Inventory
  public static void SavePlayerInventory(List<InventoryItem> items)
  {
    BinaryFormatter formatter = new BinaryFormatter();
    string path = Application.persistentDataPath + "/playerInventory.data";
    FileStream stream = new FileStream(path, FileMode.Create);

    List<SerializableInventoryItem> serializableItems = new List<SerializableInventoryItem>();
    foreach (var item in items)
    {
      serializableItems.Add(new SerializableInventoryItem(item.itemName, item.currentStackSize, item.GetType().ToString()));
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
        if (item == null)
        {
          Debug.LogError("Item not found in Resources/Items/" + serializableItem.PrefabName);
          continue;
        }

        // Create a new instance of the item and set its stack size
        item = item.GetCopy();
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
  #endregion

  #region Player Placed Items
  public static void SavePlayerPlacedItems(List<PlayerPlacedItem> gameObjects)
  {
    List<SerializedPlayerPlacedItems> serializedGameObjects = new List<SerializedPlayerPlacedItems>();

    foreach (PlayerPlacedItem obj in gameObjects)
    {
      serializedGameObjects.Add(obj.Serialize());
    }

    BinaryFormatter formatter = new BinaryFormatter();
    string path = Application.persistentDataPath + "/playerPlacedItems.data";
    FileStream stream = new FileStream(path, FileMode.Create);

    formatter.Serialize(stream, serializedGameObjects);
    stream.Close();
  }

  public static List<PlayerPlacedItem> LoadPlayerPlacedItems()
  {
    string path = Application.persistentDataPath + "/playerPlacedItems.data";
    if (File.Exists(path))
    {
      BinaryFormatter formatter = new BinaryFormatter();
      FileStream stream = new FileStream(path, FileMode.Open);

      List<SerializedPlayerPlacedItems> serializedGameObjects = formatter.Deserialize(stream) as List<SerializedPlayerPlacedItems>;
      stream.Close();

      List<PlayerPlacedItem> loadedGameObjects = new List<PlayerPlacedItem>();

      foreach (SerializedPlayerPlacedItems serializedObj in serializedGameObjects)
      {
        PlayerPlacedItem prefab = Resources.Load<PlayerPlacedItem>("Prefabs/PlayerPlacedItems/" + serializedObj.Name);
        if (prefab == null)
        {
          Debug.LogError("Prefab not found in Resources/Prefabs/PlayerPlacedItems/" + serializedObj.Name);
          continue;
        }

        PlayerPlacedItem loadedItem = prefab.Deserialize(serializedObj);
        if (loadedItem != null)
          loadedGameObjects.Add(loadedItem);
      }

      return loadedGameObjects;
    }
    else
    {
      Debug.LogError("Save file not found in " + path);
      return null;
    }
  }
  #endregion

  #region Player Actionbar
  public static void SavePlayerActionbar(List<InventoryItem> items)
  {
    BinaryFormatter formatter = new BinaryFormatter();
    string path = Application.persistentDataPath + "/playerActionbar.data";
    FileStream stream = new FileStream(path, FileMode.Create);

    List<SerializableInventoryItem> serializableItems = new List<SerializableInventoryItem>();
    foreach (var item in items)
    {
      serializableItems.Add(new SerializableInventoryItem(item.itemName, item.currentStackSize, item.GetType().ToString()));
    }

    formatter.Serialize(stream, serializableItems);
    stream.Close();
  }

  public static List<InventoryItem> LoadPlayerActionbar()
  {
    string path = Application.persistentDataPath + "/playerActionbar.data";
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
        if (item == null)
        {
          Debug.LogError("Item not found in Resources/Items/" + serializableItem.PrefabName);
          continue;
        }

        // Create a new instance of the item and set its stack size
        item = item.GetCopy();
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
  #endregion
}

#region Serializable Classes
[Serializable]
public class CustomAttributesData
{
  public List<StorageChestItemData> StorageChestContents;
}

[Serializable]
public class SerializedPlayerPlacedItems
{
  public string Name;
  public SerializableTransform Position;
  public SerializableQuaternion Rotation;
  public CustomAttributesData CustomAttributesData; // Custom attributes storage

  public SerializedPlayerPlacedItems(string name, Vector3 position, Quaternion rotation)
  {
    Name = name;
    Position = new SerializableTransform(position);
    Rotation = new SerializableQuaternion(rotation);
    CustomAttributesData = new CustomAttributesData();
  }
}


[Serializable]
public class SerializableQuaternion
{
  public float x;
  public float y;
  public float z;
  public float w;

  public SerializableQuaternion(Quaternion quaternion)
  {
    x = quaternion.x;
    y = quaternion.y;
    z = quaternion.z;
    w = quaternion.w;
  }

  public Quaternion ToQuaternion()
  {
    return new Quaternion(x, y, z, w);
  }
}

[Serializable]
public class SerializableTransform
{
  public float positionX;
  public float positionY;
  public float positionZ;

  public SerializableTransform(Vector3 position)
  {
    positionX = position.x;
    positionY = position.y;
    positionZ = position.z;
  }

  public Vector3 ToVector3()
  {
    return new Vector3(positionX, positionY, positionZ);
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
#endregion
