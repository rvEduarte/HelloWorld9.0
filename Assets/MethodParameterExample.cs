using System;
using UnityEngine;

public class MethodParameterExample : MonoBehaviour
{
    void Start()
    {
        // Call the DoSomething method and pass a custom method as a parameter
        DoSomething(CustomMethod);
    }

    // The method that accepts another method as a parameter
    public void DoSomething(Action action)
    {
        Debug.Log("Doing something before calling the custom method.");

        // Call the passed-in custom method
        action();
    }

    // The custom method that will be passed as a parameter
    public void CustomMethod()
    {
        Debug.Log("This is the custom method being called.");
    }
}
