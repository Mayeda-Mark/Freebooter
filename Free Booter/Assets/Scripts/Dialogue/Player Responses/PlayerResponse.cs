using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerResponse : ScriptableObject
{
    public string responseText;
    public virtual void ResponseButton()
    {
        Debug.Log("Default");
    }
}