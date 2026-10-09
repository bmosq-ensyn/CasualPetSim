using System.IO;
using UnityEngine;
using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
public class JsonDataService : IDataService
{
    public bool SaveData<T>(string RelativePath, T Data)
    {
        string path = Application.persistentDataPath + "/" + RelativePath;

        try
        {
            if (File.Exists(path))
            {
                Debug.LogWarning("File already exists at path: " + path + ". It will be overwritten.");
                File.Delete(path);
            }
            else
            {
                Debug.Log("No existing file at path: " + path + ". A new file will be created.");
            }

            using FileStream stream = File.Create(path);
            stream.Close();
            File.WriteAllText(path, JsonConvert.SerializeObject(Data, Formatting.Indented));
            return true;
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to save data at path: {path}. Exception: {e.Message} {e.StackTrace}");
            return false;
        }
    }

    public T LoadData<T>(string RelativePath)
    {
        string path = Application.persistentDataPath + "/" + RelativePath;

        if (!File.Exists(path))
        {
            Debug.LogWarning("No file found at path: " + path);
            return default;
        }
        
        try
        {
            T data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
            return data;
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"Failed to load data due to: {e.Message} {e.StackTrace}");
            throw e;
        }
    }
}
