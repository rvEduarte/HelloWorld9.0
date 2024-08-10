using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConveyorRight : MonoBehaviour
{
    float moveSpeed = 3.5f;
   [SerializeField] bool moveRight = true;

    void OnTriggerStay2D(Collider2D other )
    {
        Vector3 movement = (moveRight ? Vector3.right : Vector3.left) * moveSpeed * Time.deltaTime;  
        other.transform.Translate(movement); 
    }


}
