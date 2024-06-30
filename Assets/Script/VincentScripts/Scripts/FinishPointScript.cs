    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishPointScript : MonoBehaviour
{
    [SerializeField] public GameObject gameCompletion;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            gameCompletion.SetActive(true);

        }
    }
}
