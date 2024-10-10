using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerEleveHorizontal : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private GameObject leftWall;
    [SerializeField] private GameObject rightWall;

    private bool isInside = false;

    [SerializeField] private float xPositionONE;
    [SerializeField] private float yPositionONE;

    [SerializeField] private float xPositionTWO;
    [SerializeField] private float yPositionTWO;
    [SerializeField] private float time;      // 4s
    public void FixedUpdate()
    {
        if (!isInside)
        {
            if (Mathf.Approximately(platform.transform.localPosition.x, xPositionONE) && Mathf.Approximately(platform.transform.localPosition.y, yPositionONE))
            {
                leftWall.SetActive(false);
                rightWall.SetActive(false);
            }
            else if (Mathf.Approximately(platform.transform.localPosition.x, xPositionTWO) && Mathf.Approximately(platform.transform.localPosition.y, yPositionTWO))
            {
                leftWall.SetActive(false);
                rightWall.SetActive(false);
            }
            else
            {
                leftWall.SetActive(true);
                rightWall.SetActive(true);
            }
        }
    }
    public void RightElev() //MOVE RIGHT
    {
        LeanTween.moveLocal(platform, new Vector3(xPositionTWO, yPositionTWO, 0), time);
    }
    public void LeftElev() //MOVELEFT
    {
        LeanTween.moveLocal(platform, new Vector3(xPositionONE, yPositionONE, 0), time);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && Mathf.Approximately(platform.transform.localPosition.x, xPositionONE) && Mathf.Approximately(platform.transform.localPosition.y, yPositionONE))
        {
            isInside = true;
            leftWall.SetActive(true);
            rightWall.SetActive(true);

            Debug.Log("Going LEFT");
            StartCoroutine(Delay(1));
        }
        else if (collision.CompareTag("Player") && Mathf.Approximately(platform.transform.localPosition.x, xPositionTWO) && Mathf.Approximately(platform.transform.localPosition.y, yPositionTWO))
        {
            isInside = true;
            leftWall.SetActive(true);
            rightWall.SetActive(true);

            Debug.Log("Going RIGHT");
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
            LeftElev();
        }
        else if (num == 1)
        {
            RightElev();
        }
    }
}
