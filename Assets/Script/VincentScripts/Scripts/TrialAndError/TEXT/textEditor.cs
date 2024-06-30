using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class textEditor : MonoBehaviour
{
    string text = "tite";
    public TMP_Text canvasText;
    // Start is called before the first frame update
    void Start()
    {
        canvasText.text = "\nPEPE " + text;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
