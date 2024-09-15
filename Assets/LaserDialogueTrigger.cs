using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class LaserMessage
{
    public int actorId;
    public string message;
}

[System.Serializable]
public class LaserActor
{
    public string name;
    public Sprite sprite;
}
public class LaserDialogueTrigger : MonoBehaviour
{
    public Button backpackButton;
    public Button tutorialButton;

    [Header("Laser")]
    public LaserMessage[] message;
    public LaserActor[] actor;

    [Header("LaserButton")]
    public LaserMessage[] laserMessage;
    public LaserActor[] laserActor;

    public void LaserStartDialogue()
    {
        StartCoroutine(WaitForSecond());
        backpackButton.interactable = false;
        tutorialButton.interactable = false;
    }
    public void StartDialogue()
    {
        StartCoroutine(WaitSecond());
        backpackButton.interactable = false;
        tutorialButton.interactable = false;
    }

    IEnumerator WaitSecond()
    {
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<LaserDialogueManager>().OpenDialogue(message, actor);

    }

    IEnumerator WaitForSecond() 
    {
        yield return new WaitForSeconds(1);
        FindObjectOfType<LaserDialogueButtonManager>().OpenDialogue(laserMessage, laserActor);
    }


}
