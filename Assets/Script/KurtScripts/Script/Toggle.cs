using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Toggle : MonoBehaviour
{
   ToggleGroup toggleGroup;

     void Start()
    {
        toggleGroup = GetComponent<ToggleGroup>();
    }

    public void StartB()
    {
        Toggle toggle = GetComponent<Toggle>();
        Debug.Log(toggle);
    }
   
}
