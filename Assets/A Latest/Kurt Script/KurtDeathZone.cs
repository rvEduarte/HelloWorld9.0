using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KurtDeathZone : MonoBehaviour
{
    Vector2 startPoint;

    private void Start()
    {
        startPoint = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadZone"))
        {
            Die();
        }
    }

    void Die()
    {
        Respawn();
    }

    void Respawn()
    {
        transform.position = startPoint;   
    }

}
