using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Burst.CompilerServices;
using UnityEngine;

public class TriggerTutorial : MonoBehaviour
{
    public static bool disableMove;
    public static bool disableJump;

    public GameObject jumpTutorialPanel;
    void Start()
    {
        disableJump = true; //disable jumping
        disableMove = true; // enable Move;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            disableJump = false; //enable jumping 
            jumpTutorialPanel.SetActive(true);

            disableMove = false;
        }
    }

    public void EnableMove()
    {
        disableMove = true;
    }
}
