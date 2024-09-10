using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class animationPortal : MonoBehaviour
{
    public Animator anim;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name.Equals("Player"))
        {
            anim.SetTrigger("TriggerShow");
            StartCoroutine(ShowIdle());
        }
        
    }

    IEnumerator ShowIdle()
    {
        yield return new WaitForSeconds(1.5f);
        anim.SetTrigger("TriggerIdle");
    }
}
