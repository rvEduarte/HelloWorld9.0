using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFollow : MonoBehaviour
{
    public Transform aiPos;
    public float moveSpeed;

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, aiPos.position, moveSpeed * Time.deltaTime);
    }
}
