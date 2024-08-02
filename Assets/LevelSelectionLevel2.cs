using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionLevel2 : MonoBehaviour
{
    public bool unlock = false;
    public Image Unlocked;
    public LevelUnlockScriptable Level;

    public void Start()
    {
        LevelUnlock();
    }
    public void LevelUnlock()
    {
        if (Level.csharpBeginnerLevel2 == "LevelBeginner2")
        {
            Debug.Log("PASOK");
            unlock = true;
            Unlocked.gameObject.SetActive(false);

        }
    }
}
