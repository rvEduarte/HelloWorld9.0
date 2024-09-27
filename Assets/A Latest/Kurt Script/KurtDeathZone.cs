using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KurtDeathZone : MonoBehaviour
{
    Vector2 checkPoint;

    private void Start()
    {
        checkPoint = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("DeadZone"))
        {
            Die();
        }
    }

    public void UpdateCheckPoint(Vector2 pos)
    {
        checkPoint = pos;
    }

    void Die()
    {
        Respawn();
    }

    void Respawn()
    {
        transform.position = checkPoint;   
    }

}
