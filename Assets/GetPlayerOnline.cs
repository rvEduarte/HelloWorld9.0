using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using LootLocker.Requests;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class GetPlayerOnline : MonoBehaviour
{
    public LevelUnlockScriptable levelUnlockScriptable;

    public void GetPlayerFileData()
    {
        int fileID = PlayerPrefs.GetInt("PlayerSaveDataFileID");
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

            }
        }
    }
}
