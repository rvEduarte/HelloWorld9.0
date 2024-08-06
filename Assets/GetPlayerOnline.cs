using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;

public class GetPlayerOnline : MonoBehaviour
{
    public LevelUnlockScriptable levelUnlockScriptable;
    public TMP_InputField codeProgressInput;
    public void GetPlayerFileData()
    {
        // Check if the input field is empty
        if (string.IsNullOrEmpty(codeProgressInput.text))
        {
            Debug.Log("Please enter a value.");
            return;
        }

        // Parse the input field value to an integer
        if (!int.TryParse(codeProgressInput.text, out int fileID))
        {
            Debug.Log("Invalid input. Please enter a valid number.");
            return;
        }
        //int fileID = PlayerPrefs.GetInt("PlayerSaveDataFileID");
        //int fileID = int.Parse(codeProgressInput);
        LootLockerSDKManager.GetPlayerFile(fileID, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Retrieved URL");
                StartCoroutine(Download(response.url));
            }
            else
            {
                Debug.LogError("Failed to get player file: " + response.errorData.ToString());
            }
        });
    }

    private IEnumerator Download(string url)
    {
        using (UnityEngine.Networking.UnityWebRequest request = UnityEngine.Networking.UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result != UnityEngine.Networking.UnityWebRequest.Result.Success)
            {
                Debug.LogError("Failed to download file: " + request.error);
            }
            else
            {
                string fileContent = request.downloadHandler.text;
                Debug.Log("File is downloaded");
                // Do something with the content
                Debug.Log(fileContent);

                // Update the ScriptableObject with the downloaded content
                levelUnlockScriptable.UpdateFromJson(fileContent);
                DeletePlayerFile();
                SceneManager.LoadSceneAsync("MainMenu");
            }
        }
    }

    private void DeletePlayerFile()
    {
        int playerFileId = int.Parse(codeProgressInput.text);
        LootLockerSDKManager.DeletePlayerFile(playerFileId, response =>
        {
            if (response.success)
            {
                Debug.Log("Successfully deleted player file with id: " + playerFileId);
            }
            else
            {
                Debug.Log("Error deleting player file");
            }
        });
    }
}
