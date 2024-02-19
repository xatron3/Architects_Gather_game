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
      serializedGameObjects.Add(new SerializedPlayerPlacedItems(obj.ItemName, obj.transform.position, obj.transform.rotation));
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
        PlayerPlacedItem prefab = Resources.Load<PlayerPlacedItem>("Prefabs/PlayerPlacedItems/" + serializedObj.name);
        if (prefab == null)
        {
          Debug.LogError("Prefab not found in Resources/Prefabs/PlayerPlacedItems/" + serializedObj.name);
          continue;
        }

        Debug.Log("Loading " + serializedObj.name + " at " + serializedObj.position.ToVector3() + " with rotation " + serializedObj.rotation.ToQuaternion());

        // Create a new instance of the item and set its position and rotation wihout instantiating it in the scene
        prefab = GameObject.Instantiate(prefab, serializedObj.position.ToVector3(), serializedObj.rotation.ToQuaternion());

        loadedGameObjects.Add(prefab);
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
}

[Serializable]
public class SerializedPlayerPlacedItems
{
  public string name;
  public SerializableTransform position;
  public SerializableQuaternion rotation;

  public SerializedPlayerPlacedItems(string name, Vector3 position, Quaternion rotation)
  {
    this.name = name;
    this.position = new SerializableTransform(position);
    this.rotation = new SerializableQuaternion(rotation);
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

