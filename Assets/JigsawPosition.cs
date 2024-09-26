using System.Collections;
using System.Collections.Generic;
using TarodevController;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class JigsawPosition : MonoBehaviour
{
    [Header("GAME OBJECTS")]
    [SerializeField] private GameObject jigsawPanel;
    [SerializeField] private GameObject jigsaw;
    [SerializeField] private float positionGameObject;
    [SerializeField] private float positionPanel;

    [SerializeField] private float xPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            LeanTween.moveY(jigsaw, positionGameObject, 1f);
            StartCoroutine(ShowPanel());
        }
    }
    IEnumerator ShowPanel()
    {
        yield return new WaitForSeconds(1);
        LeanTween.moveLocalY(jigsawPanel, positionPanel, 0.5f);
    }
}
