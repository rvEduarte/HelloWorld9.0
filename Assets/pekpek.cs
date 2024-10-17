using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pekpek : MonoBehaviour
{
    public GameObject panel;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            panel.SetActive(true);
            // Enable player movement
            TriggerTutorial.disableMove = false;
            TriggerTutorial.disableJump = true;
        }
    }
}
