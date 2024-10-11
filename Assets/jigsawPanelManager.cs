using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jigsawPanelManager : MonoBehaviour
{
    public GameObject dimed, jigsawReward, jigsawPopUp, BGMASK;

    public void ShowPopUp()
    {
        jigsawReward.SetActive(false);
        dimed.SetActive(false);
        BGMASK.SetActive(true);
        LeanTween.scale(jigsawPopUp, Vector2.one, 0.5f);
    }
    public void HidePopUp()
    {
        BGMASK.SetActive(false);
        LeanTween.scale(jigsawPopUp, Vector2.zero, 0.5f);
    }
}
