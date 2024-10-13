using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class KurtMessage
{
    public int actorId;
    public string message;
}

[System.Serializable]
public class KurtActor
{
    public string name;
    public Sprite sprite;
}
