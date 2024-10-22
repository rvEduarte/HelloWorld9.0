using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KurtCheckPoint : MonoBehaviour
{

    GameController gameController;
    public Transform respawnPoint;
    private void Awake()
    {
        gameController = GameObject.FindGameObjectWithTag("Player").GetComponent<GameController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            Debug.Log("Checkpoint Activated");
            gameController.UpdateCheckPoint(respawnPoint.position);
        }
    }
}
 