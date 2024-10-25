using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Quiz_ComputerIntermediate : MonoBehaviour
{
    public Texture2D customCursorTexture;       // Custom cursor texture when the panel is opened
    public Vector2 cursorHotspot = Vector2.zero; // The hotspot point of the custom cursor

    [SerializeField] private List<TMP_InputField> input;
    [SerializeField] public GameObject panel;

    public GameObject hintText;

    public static bool pickUpAllowed;

    public static bool enterAllowed;

    public static bool disableE_Intermediate1;

    private void Start()
    {
        hintText.SetActive(false);
        enterAllowed = false;
        disableE_Intermediate1 = false;
    }

    private void Update()
    {
        if (disableE_Intermediate1) return;

        if (pickUpAllowed && Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("pumasok");
            TriggerTutorial.disableMove = false; //disable Move
            TriggerTutorial.disableJump = true; //disable jumping

            ShowHideScript.stopMovement = false; //The script block movement

            enterAllowed = true;
            disableE_Intermediate1 = true;

            foreach (TMP_InputField var in input)
            {
                var.enabled = true;
                Debug.Log(var);
            }
            LeanTween.scale(panel, Vector2.one, 0.5f);

            // Change the cursor to the custom texture when the panel is opened
            /*if (customCursorTexture != null)
            {
                Cursor.SetCursor(customCursorTexture, cursorHotspot, CursorMode.Auto);
            }*/
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hintText.SetActive(true);
            pickUpAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            hintText.SetActive(false);
            pickUpAllowed = false;
        }
    }
}