using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JigsawScript : MonoBehaviour
{
    [SerializeField]
    private GameObject pickUpText;

    [SerializeField]
    private GameObject Greenportal;

    public GameObject jigsawPanel;

    public GameObject jigsawPopup;

    public RunningTimerLvl2Ph1 timer;

    private bool pickUpAllowed;

    public bool isClicked = false;

    void Start()
    {
        LeanTween.scale(jigsawPanel, Vector3.zero, 0f);

        pickUpText.SetActive(false);
        Greenportal.SetActive(false);

        Debug.Log("TITE");
    }

    private void Update()
    {
        if (pickUpAllowed && Input.GetKeyDown(KeyCode.E))
        {
            jigsawPopup.SetActive(true);
            timer.isPicked = true;
            LeanTween.scale(jigsawPanel, Vector3.one, 0.8f);

            Debug.Log("TITE");
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
        Greenportal.SetActive(true);
        Destroy(gameObject);
    }  
}