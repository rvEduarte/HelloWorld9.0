using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ResetCounterObject : MonoBehaviour
{
    public CounterCount Counter1;
    string a = null;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (a == "HELLOWORLD")
        {
            Counter1.counter = 0;
        }

        else if (a == "7")
        {
            Counter1.counter = 0;
        }

        else if (a == "SPACE_HELLOWORLD")
        {
            Counter1.counter = 0;
        }

        else if (a == "SPACE_7")
        {
            Counter1.counter = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("ENTER BULLET TITE");
        //if the game object we collided with is not equal to the current game object (so anything other than itself)
        if (collision.gameObject.tag == "WHELLO")
        {
            Debug.Log("ENTER W__HELLO");
            a = "HELLOWORLD";
        }
        else if (collision.gameObject.tag == "WLHELLO")
        {
            Debug.Log("ENTER WL__HELLO");
            a = "SPACE_HELLOWORLD";          
        }

        else if (collision.gameObject.tag == "W7")
        {
            Debug.Log("ENTER W__7");
            a = "7";          
        }

        else if (collision.gameObject.tag == "WL7")
        {
            Debug.Log("ENTER W__7");
            a = "SPACE_7";           
        }
    }
}
