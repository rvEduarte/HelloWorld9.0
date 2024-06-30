using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObjectScript : MonoBehaviour
{

    public Transform posA, posB;
    public float moveSpeedObject = 9.5f;

    public Rigidbody2D body;

    private Vector3 nextPosition;
    // Start is called before the first frame update
    void Start()
    {
        nextPosition = posB.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, moveSpeedObject * Time.deltaTime);

        if(transform.position == nextPosition )
        {
            nextPosition = (nextPosition == posA.position) ? posB.position : posA.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.transform.parent = transform;
            //body = GetComponent<Rigidbody2D>();
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
