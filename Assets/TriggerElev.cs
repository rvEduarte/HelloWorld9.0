using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TriggerElev : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;

    private bool isInside = false;

    [SerializeField] private float downValue; // 28.83f
    [SerializeField] private float upValue;   // 60.77f
    [SerializeField] private float time;      // 4s
    public void FixedUpdate()
    {
        if (!isInside)
        {
            if (Mathf.Approximately(platform.transform.localPosition.y, downValue))
            {
                leftWall.SetActive(false);
                rightWall.SetActive(false);
            }
            else if (Mathf.Approximately(platform.transform.localPosition.y, upValue))
            {
                leftWall.SetActive(false);
                rightWall.SetActive(false);
            }
            /*else if(isInside)
            {
                leftWall.SetActive(true);
                rightWall.SetActive(true);
            }*/
            else
            {
                leftWall.SetActive(true);
                rightWall.SetActive(true);
            }
        }
    }
    public void UpElev()
    {
        LeanTween.moveLocalY(platform, upValue, time);
    }
    public void DownElev()
    {
        LeanTween.moveLocalY(platform, downValue, time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Mathf.Approximately(platform.transform.localPosition.y, downValue))
        {
            isInside = true;
            leftWall.SetActive(true);
            rightWall.SetActive(true);

            Debug.Log("Going DOWN");
            StartCoroutine(Delay(1));
        }
        else if(collision.CompareTag("Player") && Mathf.Approximately(platform.transform.localPosition.y, upValue))
        {
            isInside = true;
            leftWall.SetActive(true);
            rightWall.SetActive(true);

            Debug.Log("Going UP");
            StartCoroutine(Delay(0));
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //isInside = false;
        }
    }

    IEnumerator Delay(int num)
    {
        yield return new WaitForSeconds(1f);
        isInside = false;
        if (num == 0)
        {
            DownElev();
        }
        else if (num == 1)
        {
            UpElev();
        }
    }
}
