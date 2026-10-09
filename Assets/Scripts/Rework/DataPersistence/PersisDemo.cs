using UnityEngine;

public class PersisDemo : MonoBehaviour
{
    public string RelativePath = "data.json";

    private IDataService DataService = new JsonDataService();
    private DemoData DemoData = new DemoData();

    public void SerializeJson()
    {
        if (DataService.SaveData(RelativePath, DemoData))
        {
            Debug.Log("Data saved successfully.");
            Debug.Log($"Saved Data: Name={DemoData.Name}, Age={DemoData.Age}, Height={DemoData.Height}, IsActive={DemoData.IsActive}");
            Debug.Log($"File saved at: {Application.persistentDataPath}/{RelativePath}");

            try
            {
                DemoData loadedData = DataService.LoadData<DemoData>("1"+ RelativePath);
                Debug.Log($"Loaded Data: Name={loadedData.Name}, Age={loadedData.Age}, Height={loadedData.Height}, IsActive={loadedData.IsActive}");
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Failed to load data due to: {e.Message} {e.StackTrace}");
            }
        }
        else
        {
            Debug.LogError("Failed to save data.");
        }
    }

    public void ActivatePetAssembler()
    {
        PetAssembler petAssembler = new PetAssembler();
        Debug.Log("Activating PetAssembler...");
        petAssembler.Initialize();  
    }
}
public class DemoData
{
    public string Name = "Bob";
    public int Age = 23; 
    public float Height = 1.75f;
    public bool IsActive = true;
}
