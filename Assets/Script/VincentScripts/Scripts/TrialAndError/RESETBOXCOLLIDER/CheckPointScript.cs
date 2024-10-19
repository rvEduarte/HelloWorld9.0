using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPointScript : MonoBehaviour
{
    private RespawnScript respawn;

    private void Awake()
    {
        respawn = GameObject.FindGameObjectWithTag("Player").GetComponent<RespawnScript>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            respawn.respawnPoint = this.gameObject; 
        }
    }
}
