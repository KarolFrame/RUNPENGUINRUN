using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public abstract class PersistentScriptableObjects : ScriptableObject
{
    public void Save(string filename = null)
    {
        var bf = new BinaryFormatter();
        var file = File.Create(GetPath(filename));
        var json = JsonUtility.ToJson(this);

        bf.Serialize(file, json);
        file.Close();
    }
    public virtual void Load(string filename = null) 
    {
        if(File.Exists(GetPath(filename))) 
        {
            var bf = new BinaryFormatter();
            var file = File.Open(GetPath(filename), FileMode.Open);

            JsonUtility.FromJsonOverwrite((string)bf.Deserialize(file), this);
            file.Close();
        }
    }
    string GetPath(string filename= null)
    {
        var fullFileName = string.IsNullOrEmpty(filename) ? name : filename;
        return string.Format("{0}/{1}.pso", Application.persistentDataPath, fullFileName);
    }
}
