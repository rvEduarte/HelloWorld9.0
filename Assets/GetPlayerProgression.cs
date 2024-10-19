using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GetPlayerProgression : MonoBehaviour
{
    public TMP_Text level;
    public TMP_Text EXP;
    public TMP_InputField input;
    string progressionKey = "levelprogress";
    private void Start()
    {
        RegisterPlayerProgression();
        CheckXp();
    }
    public void CheckXp()
    {
        /*LootLockerSDKManager.GetPlayerInfo((response) =>
        {
            if (response.success)
            {
                Debug.Log("SUCCESSFUL GET THE XP");
                level.text = response.level.ToString();
                EXP.text = response.xp.ToString();
            }
            else
            {
                Debug.Log("PAKSHET" + response.errorData);
            }
           
        });*/
        LootLockerSDKManager.GetPlayerProgression(progressionKey, response =>
        {
            if (response.success)
            {
                Debug.Log("The player is currently level" + response.step.ToString());
                level.text = "Level: " + response.step.ToString();
                EXP.text = "Xp: " + response.points.ToString();
                if (response.next_threshold != null)
                {
                    Debug.Log("Points needed to reach next level:" + (response.next_threshold - response.points).ToString());
                }
            }
            else
            {
                Debug.Log("Failed: " + response.errorData);
            }
        });
    }
    private void RegisterPlayerProgression()
    {
        LootLockerSDKManager.RegisterPlayerProgression(progressionKey, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Progression registered");
            }
            else
            {
                Debug.Log("Error registering progression");
            }
        });
    }

    public void GiveXP()
    {
        int pointsAmount = 1;
        LootLockerSDKManager.AddPointsToPlayerProgression(progressionKey, (ulong)pointsAmount, (response) =>
        {
            if (response.success)
            {
                Debug.Log("Points added to progression");
                // If the player leveled up, the count of awarded_tiers will be greater than 0
                if (response.awarded_tiers.Count > 0)
                {
                    Debug.Log("Player leveled up");
                }
            }
            else
            {
                Debug.Log("Error adding points to progression");
            }
        });
    }
}
