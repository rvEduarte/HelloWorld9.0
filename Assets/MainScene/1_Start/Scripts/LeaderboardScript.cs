using LootLocker.Requests;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Cinemachine.DocumentationSortingAttribute;

public class LeaderboardScript : MonoBehaviour
{
    #region Variables

    public OfflineScriptableObject offlineScriptableObject;
    public SubmitLeaderBoardScript submitLead;

    private Dictionary<int, LevelData> levelDataDictionary;

    string leaderboardKey;

    [Header("Leaderboard Text")]
    public TextMeshProUGUI leaderboardLevelText;
    public TextMeshProUGUI leaderboardGamerText;
    public TextMeshProUGUI leaderboardScoreText;

    [Header("Leaderboard TimeTaken Text")]
    public TextMeshProUGUI timeTaken1;
    public TextMeshProUGUI timeTaken2;
    public TextMeshProUGUI timeTaken3;

    [Header("Leaderboard Exercise Accuracy Text")]
    public TextMeshProUGUI exerciseAccuracyPh2;
    public TextMeshProUGUI exerciseAccuracyPh3;

    [Header("Leaderboard Quiz Accuracy Text")]
    public TextMeshProUGUI quizAccuracyPh3;


    [Header("Error Handling")]
    public TextMeshProUGUI errorText;
    public GameObject errorPanel;

    #endregion

    public void Start()
    {
        // Initialize the dictionary with level-specific data
        levelDataDictionary = new Dictionary<int, LevelData>
        {
            { 1, new LevelData(
                offlineScriptableObject.TotalScore,
                offlineScriptableObject.timePhase1,
                offlineScriptableObject.timePhase2,
                offlineScriptableObject.timePhase3,
                offlineScriptableObject.exerciseAccuracyPhase2,
                offlineScriptableObject.exerciseAccuracyPhase3,
                offlineScriptableObject.quizAccuracyPhase3) },
            { 2, new LevelData(
                offlineScriptableObject.lvl2_TotalScore,
                offlineScriptableObject.lvl2_timePhase1,
                offlineScriptableObject.lvl2_timePhase2,
                offlineScriptableObject.lvl2_timePhase3,
                offlineScriptableObject.lvl2_exerciseAccuracyPhase2,
                offlineScriptableObject.lvl2_exerciseAccuracyPhase3,
                offlineScriptableObject.lvl2_quizAccuracyPhase3) }
            // Add more levels soon
        };
    }
    public void BackButtonPressed(string name)
    {
        SceneManager.LoadScene(name);
    }

    #region SubmitLeaderBoardData

    public void OffLineSubmitScore(int level)
    {
        if (levelDataDictionary.TryGetValue(level, out LevelData levelData))
        {
            // Validate data before submitting
            if (IsValidData(levelData))
            {
                Debug.Log(level);
                submitLead.GameManagerLevel(level);
                submitLead.SubmitData(
                    levelData.Score,
                    levelData.TimePhase1,
                    levelData.TimePhase2,
                    levelData.TimePhase3,
                    levelData.AccuracyExercisePhase2,
                    levelData.AccuracyExercisePhase3,
                    levelData.AccuracyQuizPhase3);

                Debug.Log($"Submitted score for level {level}");
            }
            else
            {
                Debug.LogWarning("One or more required values are null or invalid. Submission aborted.");
            }
        }
        else
        {
            Debug.LogError("Invalid level specified");
        }
    }
    private bool IsValidData(LevelData data)
    {
        return data.Score != 0 &&
               !string.IsNullOrEmpty(data.TimePhase1) &&
               !string.IsNullOrEmpty(data.TimePhase2) &&
               !string.IsNullOrEmpty(data.TimePhase3) &&
               data.AccuracyExercisePhase2 != 0 &&
               data.AccuracyExercisePhase3 != 0 &&
               data.AccuracyQuizPhase3 != 0;
    }

    #endregion


    #region GetLeaderBoardData

    public void LevelGetData()
    {
        if (leaderboardGamerText == null || leaderboardScoreText == null || timeTaken1 == null || timeTaken2 == null || timeTaken3 == null)
        {
            Debug.Log("Not assigned");
        }
        else
        {
            GetLeaderboardData();
        }
    }

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
                //show the leaderboard screen and populate it with the data 
                leaderboardGamerText.text = "PLAYER NAME";
                leaderboardScoreText.text = "SCORE";

                timeTaken1.text = "elapsed time ph1";
                timeTaken2.text = "elapsed time ph2";
                timeTaken3.text = "elapsed time ph3";

                exerciseAccuracyPh2.text = "Challenge Exercise Accuracy PH2";
                exerciseAccuracyPh3.text = "Challenge Exercise Accuracy PH3";

                quizAccuracyPh3.text = "Quiz Accuracy PH3";

                //for each item 
                foreach (LootLockerLeaderboardMember score in response.items)
                {
                    //add the score to the text
                    Debug.Log(response.items);
                    leaderboardGamerText.text += "\n" + score.rank + ". " + score.player.name;
                    leaderboardScoreText.text += "\n" + score.score.ToString();

                    // Parse metadata
                    PlayerMetaData1 metadata = JsonUtility.FromJson<PlayerMetaData1>(score.metadata);
                    timeTaken1.text += "\n" + metadata.timeTaken1;
                    timeTaken2.text += "\n" + metadata.timeTaken2;
                    timeTaken3.text += "\n" + metadata.timeTaken3;

                    exerciseAccuracyPh2.text += "\n" + metadata.accuracyExercisePh2 + "%";
                    exerciseAccuracyPh3.text += "\n" + metadata.accuracyExercisePh3 + "%";

                    quizAccuracyPh3.text += "\n" + metadata.accuracyQuizPh3 + "%";

                    //leaderboardTimeText.text += "\n" + metadata.timeTaken.ToString();
                    //leaderboardAccuracyText.text += "\n" + (metadata.accuracy * 100).ToString("F2") + "%";
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

    public void GetData(int level)
    {
        if (level == 1)
        {
            OffLineSubmitScore(level);
            leaderboardKey = "BeginnerLevel1";
            leaderboardLevelText.text = "Level1 Ranking";
        }
        else if (level == 2)
        {
            OffLineSubmitScore(level);
            leaderboardKey = "BeginnerLevel2";
            leaderboardLevelText.text = "Level2 Ranking";
        }
        // Add more levels as needed
        LevelGetData();
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

public class LevelData
{
    public int Score { get; }
    public string TimePhase1 { get; }
    public string TimePhase2 { get; }
    public string TimePhase3 { get; }
    public float AccuracyExercisePhase2 { get; }
    public float AccuracyExercisePhase3 { get; }
    public float AccuracyQuizPhase3 { get; }

    public LevelData(int scoreToSubmit, string timePhase1, string timePhase2, string timePhase3, float accuracyExercisePhase2, float accuracyExercisePhase3, float accuracyQuizPhase3)
    {
        Score = scoreToSubmit;
        TimePhase1 = timePhase1;
        TimePhase2 = timePhase2;
        TimePhase3 = timePhase3;
        AccuracyExercisePhase2 = accuracyExercisePhase2;
        AccuracyExercisePhase3 = accuracyExercisePhase3;
        AccuracyQuizPhase3 = accuracyQuizPhase3;
    }
}
