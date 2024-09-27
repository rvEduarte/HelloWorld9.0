using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KurtCheckPoint : MonoBehaviour
{
    KurtDeathZone kurtDeathZone;

    private void Awake()
    {
        kurtDeathZone = GameObject.FindGameObjectWithTag("Player").GetComponent<KurtDeathZone>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            kurtDeathZone.UpdateCheckPoint(transform.position);
        }
    }
}
 