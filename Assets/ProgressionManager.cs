using System.Collections.Generic;
using System.Text;
using UnityEngine;
using LootLocker.Requests;
using System;
using System.IO;

/*[System.Serializable]
public class PlayerProgressionData
{
    public List<int> unlockedLevels = new List<int>();
}*/

public class ProgressionManager : MonoBehaviour
{
    private string dataDirPath = "";

    private string dataFileName = "";

    public  ProgressionManager(string dataDirPath, string dataFileName)
    {
        this.dataDirPath = dataDirPath;
        this.dataFileName = dataFileName;
    }
    public void shit()
    {
        Save(GameShit);
    }

    public void Save(GameShit data)
    {
        
        string fullPath = Path.Combine(dataDirPath, dataFileName);

        try
        {
            //Create directory the file will be written 
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));

            //serialize the game data to JSON
            string dataToStore = JsonUtility.ToJson(data, true);

            //write the serialize data to the file
            using (FileStream stream = new FileStream(fullPath,FileMode.Create))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.Write(dataToStore);
                }
            }
            // Upload the serialized data to LootLocker
            SaveProgressionToLootLocker(dataToStore);
        }
        catch (Exception e)
        {
            Debug.LogError("Error occured when trying to save data to file: " + fullPath + "\n" + e);
        }

    }
    private void SaveProgressionToLootLocker(string jsonData)
    {
        try
        {
            byte[] jsonDataBytes = System.Text.Encoding.UTF8.GetBytes(jsonData);
            LootLockerSDKManager.UploadPlayerFile(
                jsonDataBytes,
                "progressionData.json",
                "application/json",
                (response) =>
                {
                    if (response.success)
                    {
                        Debug.Log("Progression data uploaded successfully.");
                    }
                    else
                    {
                        Debug.LogError("Failed to upload progression data: " + response.errorData.ToString());
                    }
                }
            );
        }
        catch (Exception ex)
        {
            Debug.LogError("An error occurred while uploading progression data: " + ex.Message);
        }
    }

}