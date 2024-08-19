using System;
using UnityEngine;
using System.IO;
using LootLocker.Requests;
using UnityEngine.SceneManagement;

public class LootlockerSceneProgress : MonoBehaviour
{
    public static LootlockerSceneProgress Instance { get; private set; }

    public LevelUnlockScriptable levelUnlockScriptable;

    private string filePath;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // PATH
    public void UploadFileFromPath(LevelUnlockScriptable levelUnlockScriptable)
    {
        string path = WriteToFile("LevelUnlockData.json", levelUnlockScriptable);
        string filePurpose = "saveFile";
        LootLockerSDKManager.UploadPlayerFile(path, filePurpose, (response) =>
        {
            // Save the file id in PlayerPrefs
            PlayerPrefs.SetInt("PlayerSaveDataFileID", response.id);
        });
    }

    public static string WriteToFile(string fileName, LevelUnlockScriptable levelUnlockScriptable)
    {
        string path = Application.persistentDataPath + "/" + fileName;

        // Convert the ScriptableObject to JSON
        string content = JsonUtility.ToJson(levelUnlockScriptable, true);

        // Write the JSON string to the file
        using (StreamWriter writer = new StreamWriter(path, false))
        {
            writer.WriteLine(content);
        }

        return path;
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
}
