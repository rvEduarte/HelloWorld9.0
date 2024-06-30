using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletDisable : MonoBehaviour
{
    public GameObject bullet1;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.gameObject.tag == "GROUNDER")
        {

            Destroy(gameObject);
        }
    }
}
