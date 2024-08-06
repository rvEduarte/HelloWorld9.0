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

    [Header("Error Handling")]
    public TextMeshProUGUI errorText;
    public GameObject errorPanel;
    public void GetPlayerFileData()
    {
        // Check if the input field is empty
        if (string.IsNullOrEmpty(codeProgressInput.text))
        {
            ShowErrorMessage("Please enter a value.");
            return;
        }

        // Parse the input field value to an integer
        if (!int.TryParse(codeProgressInput.text, out int fileID))
        {
            ShowErrorMessage("Invalid input. Please enter a valid number.");
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
                ShowErrorMessage("Error to get player file. Check your internet connection!");
                return;
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
                ShowErrorMessage("Error to get player file. Check your internet connection");
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

    // Show an error message on the screen
    public void ShowErrorMessage(string message, int showTime = 3)
    {
        //set active
        errorPanel.SetActive(true);
        errorText.text = message.ToUpper();

        //wait for 3 seconds and hide the error panel
        Invoke("HideErrorMessage", showTime);
    }

    private void HideErrorMessage()
    {
        errorPanel.SetActive(false);
    }
}
