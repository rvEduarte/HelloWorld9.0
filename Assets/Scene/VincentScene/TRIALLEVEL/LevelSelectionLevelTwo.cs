using UnityEngine;
using UnityEngine.UI;

public class LevelSelectionLevelTwo : MonoBehaviour
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
        if (Level.Level2Key == "LevelBeginner2")
        {
            Debug.Log("PASOK");
            unlock = true;
            Unlocked.gameObject.SetActive(false);
            
        }
    }
}