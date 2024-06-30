using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SettingMeru : MonoBehaviour
{
    [Header("space between menu items")]
    [SerializeField] Vector2 spacing;

    [Space]
    [Header("Main button rotation")]
    [SerializeField] float rotationDuration;
    [SerializeField] Ease easeRotation;

    [Space]
    [Header("Animation")]
    [SerializeField] float expandDuration;
    [SerializeField] float collapseDuration;
    [SerializeField] Ease expandEase;
    [SerializeField] Ease collapseEase;

    [Space]
    [Header("Fading")]
    [SerializeField] float expandFadeDuration;
    [SerializeField] float collapseFadeDuration;

    Button mainButton;
    items[] menuItems;
    bool isExpanded = false;

    Vector2 mainButtonPosition;
    int itemsCount;

    void Start()
    {
        itemsCount = transform.childCount - 1;
        menuItems = new items[itemsCount];
        for (int i = 0; i < itemsCount; i++)
        {
            menuItems[i] = transform.GetChild(i + 1).GetComponent <items> ();
        }

        mainButton =  transform.GetChild(0).GetComponent <Button> ();
        mainButton.onClick.AddListener(ToggleMenu);
        mainButton.transform.SetAsLastSibling();

        mainButtonPosition = mainButton.transform.position;

       // ResetPositions(); //reset all menu items position

    }

    /**
     * void ResetPositions()
    {
        for (int i = 0; i < itemsCount; i++)
        {
            menuItems[i].trans.position = mainButtonPosition;
        }

    } 
    **/

    void ToggleMenu()
    {
        //open
        isExpanded = !isExpanded;
        if (isExpanded)
        {
            for (int i = 0; i < itemsCount; i++)
            {
                //menuItems[i].trans.position = mainButtonPosition + spacing * (i + 1 );
                menuItems[i].trans.DOMove(mainButtonPosition + spacing * (i + 1), expandDuration).SetEase(expandEase);
                menuItems[i].img.DOFade(1f, expandFadeDuration).From(0f);
            }
        }

        else
        {
            //closed
            for (int i = 0; i < itemsCount; i++)
            {
                //menuItems[i].trans.position = mainButtonPosition;
                menuItems[i].trans.DOMove(mainButtonPosition, collapseDuration).SetEase(collapseEase);
                menuItems[i].img.DOFade(0f, collapseFadeDuration);
            }
        }

        mainButton.transform
            .DORotate(Vector3.forward * 180f, rotationDuration)
            .From(Vector3.zero)
            .SetEase(easeRotation);
    }

    void OnDestroy()
    {
        mainButton.onClick.RemoveListener(ToggleMenu);
    }
}
