using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class SwitchingTabs : MonoBehaviour
{

    EventSystem ES;
    public Selectable firstInput;

    // initialization
    void Start()
    {
        ES = EventSystem.current;
        //firstInput.Select();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            Selectable previous = ES.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (previous != null)
            {
                previous.Select();
            }
        }

        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            Selectable next = ES.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null) 
            {
                next.Select();
            }
        }
    }
}
