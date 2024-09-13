using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseControllerScript : MonoBehaviour
{
    public Texture2D customCursor; // Assign your cursor texture in the Inspector
    public Vector2 hotSpot = Vector2.zero; // Define the cursor's "hot spot" (the precise click point)
    public CursorMode cursorMode = CursorMode.Auto; // Choose between Auto or ForceSoftware

    // Optionally, you can change the cursor on specific events (e.g., mouse hover)
    public void OnMouseEnter()
    {
        // Calculate the hotspot (center of the texture)
        Vector2 hotSpot = new Vector2(customCursor.width / 2, customCursor.height / 2);
        // Change the cursor
        Cursor.SetCursor(customCursor, hotSpot, cursorMode);
    }

    public void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, cursorMode); // Revert to default cursor
    }
}
