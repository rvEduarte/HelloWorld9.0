using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerDragAndDropTutorial : MonoBehaviour
{
    public GameObject dragAndDropTutorialPanel;
    public GameObject triggerDragAndDrop;
    public GameObject bgMask;
    // Start is called before the first frame update
    void Start()
    {
        LeanTween.scale(dragAndDropTutorialPanel, Vector3.zero, 0f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            TriggerTutorial.disableMove = false; //disable Move
            TriggerTutorial.disableJump = true; //disable jumping
            dragAndDropTutorialPanel.SetActive(true);
            bgMask.SetActive(true);
            LeanTween.scale(dragAndDropTutorialPanel, new Vector3(1, 1, 1), 1f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
            StartCoroutine(DisableDragAndDropTutorial());
        }
    }

    IEnumerator DisableDragAndDropTutorial()
    {
        // waiting state
        yield return new WaitForSeconds(0.5f);
        //dragAndDropTutorialPanel.SetActive(false);
        triggerDragAndDrop.SetActive(false);
    }
}
