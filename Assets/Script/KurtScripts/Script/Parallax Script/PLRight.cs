using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLRight : MonoBehaviour
{
    Material mat2;
    float distance2;

    [Range(0f, 0.5f)]
    public float speed2 = 0.1f;

    void Start()
    {
        mat2 = GetComponent<Renderer>().material;
    }

    void Update()
    {
        distance2 += Time.deltaTime * speed2;
        mat2.SetTextureOffset("_MainTex", Vector2.left * distance2);
    }

}
