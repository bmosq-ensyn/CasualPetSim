using System.Collections;
using UnityEngine;

public class PersistenceModulator : MonoBehaviour
{
    private IDataService DataService = new JsonDataService();

    IEnumerator Save<T>(T data, string RelativePath)
    {
        Debug.Log($"Saving {data}.");
        if(DataService.SaveData(RelativePath, data))
        {
            Debug.Log($"File saved at: {Application.persistentDataPath}/{RelativePath}");
        }
        else
        {
            Debug.LogError("Failed to save data.");
        }
        yield break;
    }
    IEnumerator Load<T>(string RelativePath)
    {
        Debug.Log($"Loading data from {RelativePath}.");
        try
        {
            T loadedData = DataService.LoadData<T>(RelativePath);
            Debug.Log($"Loaded Data: {loadedData}");
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Failed to load data due to: {e.Message} {e.StackTrace}");
        }
        yield break;
    }

    #region Save
    public void SaveWorldState()
    {

    }
    #endregion

    #region Load
    public void LoadWorldState()
    {

    }
    #endregion
}
