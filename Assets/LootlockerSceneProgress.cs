using System;
using UnityEngine;
using System.IO;
using LootLocker.Requests;
using UnityEngine.SceneManagement;

public class LootlockerSceneProgress : MonoBehaviour
{
    public LevelUnlockScriptable levelUnlockScriptable;

    public static string filePath;

    private void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "LevelUnlockData.json");
    }
    private void Awake()
    {
        filePath = Path.Combine(Application.persistentDataPath, "LevelUnlockData.json");
    }

    // PATH
    public void UploadFileFromPath(LevelUnlockScriptable levelUnlockScriptable)
    {
        filePath = WriteToFile("LevelUnlockData.json", levelUnlockScriptable);
        string filePurpose = "saveFile";
        LootLockerSDKManager.UploadPlayerFile(filePath, filePurpose, (response) =>
        {
            // Save the file id in PlayerPrefs
            PlayerPrefs.SetInt("PlayerSaveDataFileID", response.id);
        });
    }

    public static string WriteToFile(string fileName, LevelUnlockScriptable levelUnlockScriptable)
    {
        filePath = Path.Combine(Application.persistentDataPath, fileName);

        // Convert the ScriptableObject to JSON
        string content = JsonUtility.ToJson(levelUnlockScriptable, true);

        // Write the JSON string to the file
        using (StreamWriter writer = new StreamWriter(filePath, false))
        {
            writer.WriteLine(content);
        }

        return filePath;
    }

    public void UpdatePlayerFile()
    {
        string fileName = "LevelUnlockData.json";
        string filePath = WriteToFile(fileName, levelUnlockScriptable);
        int fileID = PlayerPrefs.GetInt("PlayerSaveDataFileID");

        LootLockerSDKManager.UpdatePlayerFile(fileID, filePath, (response) => {
            if (response.success)
            {
                Debug.Log("File was updated successfully!");
            }
            else
            {
                Debug.LogError("Failed to update file: " + response.errorData);
            }
        });
    }

    // Load data from a local file
    public void LoadFromLocalFile()
    {
        // Ensure filePath is set before attempting to read
        //if (string.IsNullOrEmpty(filePath))
        //{
        //    filePath = Path.Combine(Application.persistentDataPath, "LevelUnlockData.json");
        //}

        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            levelUnlockScriptable.UpdateFromJson(jsonData);
            Debug.Log("Data loaded from local file: " + filePath);
        }
        else
        {
            Debug.LogWarning("For load data => No local save file found at " + filePath);
        }
    }
    // Save data to a local file
    public void SaveToLocalFile()
    {
        LoadFromLocalFile();
        filePath = WriteToFile("LevelUnlockData.json", levelUnlockScriptable);
        Debug.Log("Data saved locally to: " + filePath);
    }
    private void OnApplicationQuit()
    {
        SaveToLocalFile();
    }
    // Method to delete the JSON file
    public void DeleteData()
    {
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
            Debug.Log("File deleted: " + filePath);
        }
        else
        {
            Debug.LogWarning("No file found to delete at " + filePath);
        }
    }

    // Reset the values in the ScriptableObject and save locally and/or upload to the server
    public void ResetProgress()
    {
        // Reset the ScriptableObject values
        levelUnlockScriptable.ResetValues();
        filePath = WriteToFile("LevelUnlockData.json", levelUnlockScriptable);
        // Save the reset data to a local file
        SaveToLocalFile();
    }
}
