using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class TriButtonX : MonoBehaviour
{
    public Button yourButton1;
    void Start()
    {
        Button btn = yourButton1.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Debug.Log("You have clicked the buttonX!");
        //GameObject.Find("DraggableSlots").transform.localScale = new Vector3(0, 0, 0);
        //GameObject.Find("ButtonX").transform.localScale = new Vector3(0, 0, 0);
        //GameObject.Find("ButtonCircle").transform.localScale = new Vector3((float)0.5, (float)2.5, 1);
        GetComponent<CanvasRenderer>().cull = false;
    }
}
