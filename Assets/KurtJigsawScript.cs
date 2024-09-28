using System.Collections;
using System.Collections.Generic;
using TarodevController;
using UnityEngine;

public class KurtJigsawScript : MonoBehaviour
{
    public PlayerController playerController;
    [SerializeField]
    private GameObject pickUpText;

    public GameObject jigsawPanel;

    //public RunningTimerLevel1Ph1 timer;

    public GameObject jigsaw;

    private bool pickUpAllowed;

    public bool isClicked = false;

    //public GameObject tutorialPanel;
    //public GameObject backpackPanel;
    //public GameObject timerPanel;
    void Start()
    {
        //LeanTween.scale(jigsawPanel, Vector3.zero, 0f);
        jigsawPanel.SetActive(false);
        pickUpText.SetActive(false);

        Debug.Log("TITE");
    }

    private void Update()
    {
        if (pickUpAllowed && Input.GetKeyDown(KeyCode.E))
        {

            //timer.isPicked = true;
            //LeanTween.scale(timerPanel, Vector3.zero, 0.5f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
            //LeanTween.scale(tutorialPanel, Vector3.zero, 0.5f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
            //LeanTween.scale(backpackPanel, Vector3.zero, 0.5f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
            //LeanTween.scale(jigsawPanel, Vector3.one, 0.5f);
            jigsawPanel.SetActive(true);
            TriggerTutorial.disableMove = false; //disable Move
            TriggerTutorial.disableJump = true; //disable jumping

            Debug.Log("TITE");
            Destroy(jigsaw);
        }
    }
    /* public void ClickTask()
    {
        Debug.Log("PEKPEK");
        LeanTween.scale(jigsawPanel, Vector3.zero, 1f).setEaseInBack();
        PickUp();
    }*/

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            pickUpText.SetActive(true);
            pickUpAllowed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {

            pickUpText.SetActive(false);
            pickUpAllowed = false;
        }
    }
}
