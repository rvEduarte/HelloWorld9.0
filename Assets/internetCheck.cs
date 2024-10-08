using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class InternetCheck : MonoBehaviour
{
    public TMP_Text debuggerText;
    // URL of a reliable server (can be adjusted if needed)
    private string testUrl = "https://www.google.com";

    public void CheckInternetConnection()
    {
        // First, check if there's any network connection at all
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("No network connection (Wi-Fi, LAN, or mobile).");
            debuggerText.text = "No network connection (Wi-Fi, LAN, or mobile).";
        }
        else
        {
            // If connected, check if the network has internet access by making a request
            StartCoroutine(CheckInternetAccess());
        }
    }

    private IEnumerator CheckInternetAccess()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(testUrl))
        {
            // Set a timeout (e.g., 5 seconds) for the request
            request.timeout = 5; // Time in seconds

            // Send the request
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                // Network is available but there is no internet access
                Debug.Log("Connected to a network, but no internet access (or the test URL is unreachable).");
                debuggerText.text = "Connected to a network, but no internet access (or the test URL is unreachable).";
            }
            else
            {
                // Network has internet access
                Debug.Log("Network has internet access.");
                if (Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork)
                {
                    Debug.Log("Connected via mobile data and has internet access.");
                    debuggerText.text = "Connected via mobile data and has internet access.";
                }
                else if (Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
                {
                    Debug.Log("Connected via Wi-Fi/LAN and has internet access.");
                    debuggerText.text = "Connected via Wi-Fi/LAN and has internet access.";
                }
            }
        }
    }
}
