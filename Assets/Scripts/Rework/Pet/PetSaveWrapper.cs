using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PetSaveWrapper
{
    public static Dictionary<string, PetStats> pets = new Dictionary<string, PetStats>();
    public string relativePath = "PetData.json";

    public void SerializeData()
    {
        IDataService dataService = new JsonDataService();
        if (dataService.SaveData(relativePath, pets))
        {
            Debug.Log("Pet data saved successfully.");
            Debug.Log($"File saved at: {Application.persistentDataPath}/{relativePath}");
        }
        else
        {
            Debug.LogError("Failed to save pet data.");
        }
    }

    // To be called on game load
    public void DeserializeData()
    {
        IDataService dataService = new JsonDataService();

        try
        {
            PetSaveWrapper pets = dataService.LoadData<PetSaveWrapper>(relativePath);
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to load data due to: {e.Message} {e.StackTrace}");
        }
    }

    // Add pet to the dictionary
    public void AddPet(PetStats petStats)
    {
        if (!pets.ContainsKey(petStats.ID))
        {
            pets.Add(petStats.ID, petStats);
            Debug.Log($"Pet with ID {petStats.ID} added.");
        }
        else
        {
            Debug.LogWarning($"Pet with ID {petStats.ID} already exists. Use UpdatePet to modify it.");
        }
    }
}