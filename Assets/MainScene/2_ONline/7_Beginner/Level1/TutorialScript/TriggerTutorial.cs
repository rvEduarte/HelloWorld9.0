using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerTutorial : MonoBehaviour
{
    [SerializeField]public static bool disableMove;
    [SerializeField]public static bool disableJump;

    public GameObject jumpTutorialPanel;
    public GameObject triggerJump;
    void Start()
    {
        disableJump = true; //disable jumping
        LeanTween.scale(jumpTutorialPanel, Vector3.zero, 0f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            disableJump = false; //enable jumping 
            jumpTutorialPanel.SetActive(true);
            LeanTween.scale(jumpTutorialPanel, new Vector3(1, 1, 1), 1f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);


            disableMove = false;
        }
    }

    public void EnableMove()
    {
        LeanTween.scale(jumpTutorialPanel, Vector3.zero, 1f).setEase(LeanTweenType.easeOutQuint).setIgnoreTimeScale(true);
        disableMove = true;
        StartCoroutine(DisableJumpTutorial());
    }

    IEnumerator DisableJumpTutorial()
    {
        // waiting state
        yield return new WaitForSeconds(0.5f);
        jumpTutorialPanel.SetActive(false);
        triggerJump.SetActive(false);
    }
}
