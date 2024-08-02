using System;
using UnityEngine;
using System.IO;
using LootLocker.Requests;


public class EventManager : MonoBehaviour
{
    public static event Action OnLevelUnlock;

    public static void LevelUnlock()
    {
        OnLevelUnlock?.Invoke();
    }

    public LevelUnlockScriptable levelUnlockScriptable;

    private string filePath;

    private void Start()
    {
        filePath = Path.Combine(Application.persistentDataPath, "LevelUnlockData.json");
    }

    public void Save()
    {
        string jsonData = JsonUtility.ToJson(levelUnlockScriptable, true);
        File.WriteAllText(filePath, jsonData);
        Debug.Log("Data saved to: " + filePath);
    }

    public void Load()
    {
        if (File.Exists(filePath))
        {
            string jsonData = File.ReadAllText(filePath);
            JsonUtility.FromJsonOverwrite(jsonData, levelUnlockScriptable);
            Debug.Log("Data loaded from: " + filePath);
        }
        else
        {
            Debug.LogError("No save file found at: " + filePath);
        }
    }

    // PATH
    public void UploadFileFromPath(LevelUnlockScriptable levelUnlockScriptable)
    {
        string path = WriteToFile("save.txt", levelUnlockScriptable);
        string filePurpose = "saveFile";
        LootLockerSDKManager.UploadPlayerFile(path, filePurpose, (response) =>
        {
            // Save the file id in PlayerPrefs
            PlayerPrefs.SetInt("PlayerSaveDataFileID",response.id);
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
}
