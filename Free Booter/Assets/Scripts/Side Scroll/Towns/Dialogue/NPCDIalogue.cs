using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NPCDialogue", menuName = "NPCDialogues")]
public class NPCDialogue : ScriptableObject
{
    public string dialogue;
    public PlayerResponse[] playerResponses;
}
