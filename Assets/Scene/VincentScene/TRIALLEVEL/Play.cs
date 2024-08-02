using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Play : MonoBehaviour
{
    public LevelUnlockScriptable Level;

    public void PlayButton()
    {
        Level.Level2Key = "LevelBeginner2";
        //EventManager.LevelUnlock();
    }
}
