using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Kurt_JigsawLevel2Ph1 : MonoBehaviour
{
    [SerializeField]
    private GameObject pickUpText, jigsaw;

    public GameObject jigsawPanel, dimed;

    public RunningTimerLevel2Ph2 timer;

    private bool pickUpAllowed;

    public bool isClicked = false;

    public GameObject tutorialPanel;
    public GameObject backpackPanel;

    public RvComputer rvComp;
    public GameObject timerPanel;
    void Start()
    {
        LeanTween.scale(jigsawPanel, Vector3.zero, 0f);
        dimed.SetActive(false);

        pickUpText.SetActive(false);

        rvComp.enabled = false;

        Debug.Log("TITE");
    }

    private void Update()
    {
        if (pickUpAllowed && Input.GetKeyDown(KeyCode.E))
        {

            timer.isPicked = true;
            LeanTween.scale(timerPanel, Vector3.zero, 0.5f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
            LeanTween.scale(tutorialPanel, Vector3.zero, 0.5f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
            LeanTween.scale(backpackPanel, Vector3.zero, 0.5f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
            LeanTween.scale(jigsawPanel, Vector3.one, 0.5f);
            dimed.SetActive(true);
            TriggerTutorial.disableMove = false; //disable Move
            TriggerTutorial.disableJump = true; //disable jumping

            rvComp.enabled = true;

            Debug.Log("TITE");
            PickUp();
        }
    }
    public void ClickTask()
    {
        Debug.Log("PEKPEK");
        LeanTween.scale(jigsawPanel, Vector3.zero, 1f).setEaseInBack();
        PickUp();
    }

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

    private void PickUp()
    {
        Destroy(jigsaw);
    }
}
