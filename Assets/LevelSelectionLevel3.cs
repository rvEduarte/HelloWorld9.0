using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionLevel3 : MonoBehaviour
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
        if (Level.csharpBeginnerLevel3 == "LevelBeginner3")
        {
            Debug.Log("PASOK");
            unlock = true;
            Unlocked.gameObject.SetActive(false);

        }
    }
}
