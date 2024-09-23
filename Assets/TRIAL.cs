using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TRIAL : MonoBehaviour
{
    public CinemachineVirtualCamera vCam;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            vCam.Priority = 12;
        }
    }
}
