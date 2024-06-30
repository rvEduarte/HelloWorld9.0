using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BRMusic : MonoBehaviour
{
    private static BRMusic instance;

    void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(instance);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
