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
}
