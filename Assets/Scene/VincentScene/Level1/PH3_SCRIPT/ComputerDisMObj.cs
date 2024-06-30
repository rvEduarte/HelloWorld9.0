using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Linq;
using TarodevController;
using UnityEngine;
using UnityEngine.InputSystem;

public class ComputerDisMObj : MonoBehaviour
{
    public GameObject exla;

    public GameObject Hint;

    public GameObject answerPanel;

    public GameObject player;

    public bool inside = false;

    public GameObject movingObject;
    void Start()
    {
        answerPanel.SetActive(false);
        movingObject.SetActive(false);
    }

    private void Update()
    {
        if (inside == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                player.SetActive(false);
                answerPanel.SetActive(true);
                Debug.Log("TITE SI KARL");
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player2.0"))
        {
            exla.SetActive(false);
            Hint.SetActive(true);

            inside = true;
            //movingObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player2.0"))
        {
            exla.SetActive(true);
            Hint.SetActive(false);

            //movingObject.SetActive(true);
        }
    }


}
