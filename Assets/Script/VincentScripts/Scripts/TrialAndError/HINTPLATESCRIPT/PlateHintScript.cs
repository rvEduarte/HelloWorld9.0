using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateHintScript : MonoBehaviour
{
    public ShowHideScript showHideScript;
    public BoxCollider2D box2D;
    public GameObject EXLA;

    public GameObject Hint;

    public GameObject HintPanel;

    public bool inside = false;

    // Start is called before the first frame update
    void Start()
    {
        HintPanel.SetActive(false);
    }

    // Update is called once per frame
    private void Update()
    {
        if (inside == true)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                box2D.enabled = false;
                showHideScript.ShowSpecificPanel(HintPanel);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player2.0"))
        {
            
            EXLA.SetActive(false);
            Hint.SetActive(true);

            inside = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player2.0"))
        {
            Hint.SetActive(false);
            HintPanel.SetActive(false);
            inside = false;

        }
    }
}
