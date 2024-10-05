using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jigsawPanelManager : MonoBehaviour
{
    public GameObject dimed, jigsawReward, jigsawPopUp;

    public void ShowPopUp()
    {
        jigsawReward.SetActive(false);
        dimed.SetActive(false);
        jigsawPopUp.SetActive(true);
    }
    public void HidePopUp()
    {
        jigsawPopUp.SetActive(false);
    }
}
