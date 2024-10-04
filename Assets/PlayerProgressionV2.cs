using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerProgressionV2 : MonoBehaviour
{
    public TMP_Text currentLevel, currentXp;
    void Start()
    {
        
    }

    public void SubmitXp()
    {
        int scoreToSubmit = 100;

        LootLockerSDKManager.SubmitXp(scoreToSubmit, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Successful");
            }
            else
            {
                Debug.Log("Error: " + response.errorData);
            }
        });
    }

    public void GetXp()
    {
        LootLockerSDKManager.GetPlayerInfo((response) =>
        {
            if (response.success)
            {
                Debug.Log("Successful");
                currentLevel.text = response.level.ToString();
                currentXp.text = response.xp.ToString();
            }
            else
            {
                Debug.Log("Error: " + response.errorData);
            }
        });
    }

}
