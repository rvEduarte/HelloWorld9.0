using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ElevatorScript : MonoBehaviour
{
    public Rigidbody2D body;

    private Vector3 nextPosition;

    public GameObject platform;


    private void Start()
    {
    }

    public void FinishQuiz()
    {
        LeanTween.moveLocalY(platform, 28.83f, 2.5f);
    }

    public void UpElev()
    {
        LeanTween.moveLocalY(platform, 60.77f, 2.5f);
    }
    public void DownElev()
    {
        LeanTween.moveLocalY(platform, 28.83f, 2.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = transform;
            body.interpolation = RigidbodyInterpolation2D.None;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = null;
            body.interpolation = RigidbodyInterpolation2D.Interpolate;
        }
    }

}
