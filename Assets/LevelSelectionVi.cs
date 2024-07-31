using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LevelSelectionVi : MonoBehaviour
{
    [SerializeField]
    bool unlock;

    public LevelUnlockScriptable Level;

    public void LevelUnlock()
    {
        if (Level.Level2Key == "")
        {
            unlock = true;
        }
        else if (Level.Level3Key == "")
        {
            unlock = true;
        }
    }

}
