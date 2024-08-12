using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ranking : MonoBehaviour
{
    [Header("Leaderboard Text")]
    public TextMeshProUGUI leaderboardLevelText;

    [Header("Error Handling")]
    public TextMeshProUGUI errorText;
    public GameObject errorPanel;

    string leaderboardKey;
    public addingimage addingimage;
    // Start is called before the first frame update
    void Start()
    {
        leaderboardKey = PlayerPrefs.GetString("leaderboardKey");
        leaderboardLevelText.text = PlayerPrefs.GetString("level");
        GetLeaderboardData();
    }

    public void GoToScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    #region Get Leaderboard
    public void GetLeaderboardData()
    {
        //how many scores to retrieve
        int count = 10;

        LootLockerSDKManager.GetScoreList(leaderboardKey, count, 0, (response) =>
        {
            if (response.success)
            {
                // Leaderboard was retrieved
                Debug.Log("Leaderboard was retrieved");

                //for each item 
                foreach (LootLockerLeaderboardMember score in response.items)
                {
                    if (score.rank == 1)
                    {
                        addingimage.BackGroundImage();

                        addingimage.LevelFirstMedal();

                        addingimage.PlayerName(score.player.name);

                        addingimage.ScrollRectTransform();

                        addingimage.ViewPortObject();

                        addingimage.ContentObject();

                        addingimage.TextScoreImage(score.score.ToString());

                        PlayerMetaData1 metadata = JsonUtility.FromJson<PlayerMetaData1>(score.metadata);
                        addingimage.TextTimerImage(metadata.timeTaken1);
                        addingimage.TextTimerImage(metadata.timeTaken2);
                        addingimage.TextTimerImage(metadata.timeTaken3);

                        addingimage.TextExerciseImage(metadata.accuracyExercisePh2.ToString() + "%");
                        addingimage.TextExerciseImage(metadata.accuracyExercisePh3.ToString() + "%");

                        addingimage.TextQuizImage(metadata.accuracyQuizPh3.ToString() + "%");

                    }
                    else if (score.rank == 2)
                    {
                        addingimage.BackGroundImage();

                        addingimage.LevelSecondMedal();

                        addingimage.PlayerName(score.player.name);

                        addingimage.ScrollRectTransform();

                        addingimage.ViewPortObject();

                        addingimage.ContentObject();

                        addingimage.TextScoreImage(score.score.ToString());

                        PlayerMetaData1 metadata = JsonUtility.FromJson<PlayerMetaData1>(score.metadata);
                        addingimage.TextTimerImage(metadata.timeTaken1);
                        addingimage.TextTimerImage(metadata.timeTaken2);
                        addingimage.TextTimerImage(metadata.timeTaken3);

                        addingimage.TextExerciseImage(metadata.accuracyExercisePh2.ToString() + "%");
                        addingimage.TextExerciseImage(metadata.accuracyExercisePh3.ToString() + "%");

                        addingimage.TextQuizImage(metadata.accuracyQuizPh3.ToString() + "%");
                    }
                    else if (score.rank == 3)
                    {
                        addingimage.BackGroundImage();

                        addingimage.LevelThirdMedal();

                        addingimage.PlayerName(score.player.name);

                        addingimage.ScrollRectTransform();

                        addingimage.ViewPortObject();

                        addingimage.ContentObject();

                        addingimage.TextScoreImage(score.score.ToString());

                        PlayerMetaData1 metadata = JsonUtility.FromJson<PlayerMetaData1>(score.metadata);
                        addingimage.TextTimerImage(metadata.timeTaken1);
                        addingimage.TextTimerImage(metadata.timeTaken2);
                        addingimage.TextTimerImage(metadata.timeTaken3);

                        addingimage.TextExerciseImage(metadata.accuracyExercisePh2.ToString() + "%");
                        addingimage.TextExerciseImage(metadata.accuracyExercisePh3.ToString() + "%");

                        addingimage.TextQuizImage(metadata.accuracyQuizPh3.ToString() + "%");
                    }
                    else
                    {
                        addingimage.BackGroundImage();

                        addingimage.LevelNumber(score.rank.ToString());

                        addingimage.PlayerName(score.player.name);

                        addingimage.ScrollRectTransform();

                        addingimage.ViewPortObject();

                        addingimage.ContentObject();

                        addingimage.TextScoreImage(score.score.ToString());

                        PlayerMetaData1 metadata = JsonUtility.FromJson<PlayerMetaData1>(score.metadata);
                        addingimage.TextTimerImage(metadata.timeTaken1);
                        addingimage.TextTimerImage(metadata.timeTaken2);
                        addingimage.TextTimerImage(metadata.timeTaken3);

                        addingimage.TextExerciseImage(metadata.accuracyExercisePh2.ToString() + "%");
                        addingimage.TextExerciseImage(metadata.accuracyExercisePh3.ToString() + "%");

                        addingimage.TextQuizImage(metadata.accuracyQuizPh3.ToString() + "%");
                    }
                }
            }
            else
            {
                // Error
                Debug.Log(response.errorData.ToString());
                if (response.errorData.ToString().Contains("message"))
                {
                    showErrorMessage(extractMessageFromLootLockerError(response.errorData.ToString()));
                }
                else
                {
                    showErrorMessage("Error retrieving leaderboard");
                }
            }
        });
    }

    #endregion

    #region ErroMessageHandler

    // Show an error message on the screen
    public void showErrorMessage(string message, int showTime = 3)
    {
        //set active
        errorPanel.SetActive(true);
        errorText.text = message.ToUpper();
        //wait for 3 seconds and hide the error panel
        Invoke("hideErrorMessage", showTime);
    }

    private void hideErrorMessage()
    {
        errorPanel.SetActive(false);
    }

    private string extractMessageFromLootLockerError(string rawError)
    {
        // Find the start index of the message
        int startIndex = rawError.IndexOf("\"") + 1; // Skip the first quote
        if (startIndex == 0)
        {
            return "Message not found"; // Handle case where the first quote is not found
        }

        // Find the end index of the message
        int endIndex = rawError.IndexOf("\"", startIndex); // Find the closing quote
        if (endIndex == -1)
        {
            return "Message not properly terminated"; // Handle case where the message is not properly terminated
        }

        // Extract the message
        string message = rawError.Substring(startIndex, endIndex - startIndex);

        return message;
    }

    #endregion
}
