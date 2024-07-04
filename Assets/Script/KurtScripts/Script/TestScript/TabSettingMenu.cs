using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TabSettingMenu : MonoBehaviour
{

    public GameObject[] Tabs;
    public Image[] tabButtons;
    public Sprite inactiveTabBG, activeTabBG;
    public Vector2 inactiveBtnSize, activeBtnSize;


    public void Switch2Tab(int tabID)
    {
        foreach (GameObject go in Tabs)
        {
            go.SetActive(false);
        }

        Tabs[tabID].SetActive(true);

        foreach (Image img in tabButtons)
        {
            img.sprite = inactiveTabBG;
            img.rectTransform.sizeDelta = inactiveBtnSize;
        }

        tabButtons[tabID].sprite = activeTabBG;
        tabButtons[tabID].rectTransform.sizeDelta = activeBtnSize;

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
