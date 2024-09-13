using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    [Header("Laser")]
    public LaserMessage[] message;
    public LaserActor[] actor;

    [Header("LaserButton")]
    public LaserMessage[] laserMessage;
    public LaserActor[] laserActor;

    public void LaserStartDialogue()
    {
        FindObjectOfType<LaserDialogueButtonManager>().OpenDialogue(laserMessage, laserActor);
    }
    public void StartDialogue()
    {
        StartCoroutine(WaitSecond());
    }

    IEnumerator WaitSecond()
    {
        yield return new WaitForSeconds(0.5f);
        FindObjectOfType<LaserDialogueManager>().OpenDialogue(message, actor);
    }


}
