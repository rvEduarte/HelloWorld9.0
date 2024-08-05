using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TRIALREMEMBERDATA : MonoBehaviour
{
    public TextMeshProUGUI nameas;
    void Start()
    {
        
    }
    public void changename()
    {
        PlayerPrefs.SetString("Changename", nameas.ToString());
    }

    public void rememberData()
    {

    }
}
