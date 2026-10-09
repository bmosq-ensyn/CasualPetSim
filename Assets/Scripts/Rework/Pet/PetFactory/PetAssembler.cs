using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class PetAssembler
{ 
    private IDataService DataService = new JsonDataService();

    public void Initialize()
    {
        // Implementation for initializing the pet with the given ID or assigning a new ID if none is provided
        IDPackage idPackage = new IDPackage(this);
        idPackage.Initialize();

        if (idPackage.existingIDs != null && idPackage.existingIDs.Count > 0)
        {
            foreach (string existingID in idPackage.existingIDs)
            {
                Debug.Log($"Existing ID: {existingID}");
                // Assign pet stats + etc. for the existing pet here
            }
        }
        else
        {
            // No existing IDs, assign a new one
            string newID = Guid.NewGuid().ToString();
            idPackage.existingIDs.Add(newID);
            Debug.Log($"Assigned new ID: {newID}");

            DataService.SaveData("PetIDs.json", idPackage); // Save the updated ID package to the JSON file

            // Assign pet stats + etc. for the new pet here
        }
    }
    void CollectPetPrefab(string PetType)
    {
        // Implementation for collecting pet prefabs based on the PetType
    }
    void AssignPetStats(string ID) // Wellness, Name, Level, etc.
    {
        // Implementation for assigning pet stats based on the ID or creating new stats if none exist

        string petID;

        string petName;
        
        int petLevel;
        float petExperience;

        float petHappiness;
        float petHunger;
        float petDirtiness;
        float petSleepiness;
    }
    private class IDPackage
    {
        public List<string> existingIDs = new List<string>();

        private string RelativePath = "PetIDs.json";
        private PetAssembler parent; 

        public IDPackage(PetAssembler parent)
        {
            // Initialize the parent reference if needed
            this.parent = parent;
        }
        public void Initialize()
        {
            // Attempt to load existing IDs from the JSON file
            try
            {
                var loadedPackage = parent.DataService.LoadData<IDPackage>(RelativePath); // Call to load the existing IDs from the JSON file once

                if (loadedPackage != null)
                {
                    existingIDs = loadedPackage.existingIDs;
                    Debug.Log("Loaded existing IDs: " + string.Join(", ", existingIDs));
                }
                else
                {
                    parent.DataService.SaveData(RelativePath, this);
                    Debug.Log("No existing IDs found. Created new ID package.");
                }
            }
            catch (Exception e)
            {
                Debug.LogError($"Failed to load existing IDs due to: {e.Message} {e.StackTrace}");
            }
        }
    }
}
