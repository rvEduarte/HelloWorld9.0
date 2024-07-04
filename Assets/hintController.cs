using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class hintController : MonoBehaviour
{
    public TextMeshProUGUI hintText1;

    public int number;

    public StartHintButton startHintButton;

    //public Button rightButton;

    // public Button leftButton;

    private List<string> hints = new List<string>
    {
        "\n\n<color=red>Write</color> - is like typing words on a typewriter without pressing Enter. It just keeps adding words next to each other on the same line.",
        "\n\n<color=blue>WriteLine</color> - is like typing words on a typewriter and then pressing Enter after each line. It makes each new set of words start on a fresh line.",
        "\n\nIn C#, You can also output numbers, and perform <color=red> mathematical calculations"
        // Add more hints as needed

    };


    public void RightButton()
    {
        Debug.Log("Number: " + number);
        if (startHintButton.isClicked == true) // Assuming 'isClicked' is a boolean property in StartHintButton.
        {
            if (number < hints.Count - 1)
            {
                number++;
                hintText1.text = hints[number];
            }
            else
            {
                Debug.Log("No more hints to the right.");
            }
        }
    }

    public void LeftButton()
    {
        Debug.Log("Number: " + number);
        if (startHintButton.isClicked == true) // Assuming 'isClicked' is a boolean property in StartHintButton.
        {
            if (number > 0)
            {
                number--;
                hintText1.text = hints[number];
            }
            else
            {
                Debug.Log("No more hints to the left.");
            }
        }
    }
}
