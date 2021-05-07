using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogue
{
    public int id;
    public string dialogueString;
    public bool hasQuestDialogue;
    public Dictionary<int, string> questDialogue = new Dictionary<int, string>();

    public Dialogue(int id, string dialogueString, bool hasQuestDialogue, Dictionary<int, string> questDialogue)
    {
        this.id = id;
        this.dialogueString = dialogueString;
        this.hasQuestDialogue = hasQuestDialogue;
        this.questDialogue = questDialogue;
    }
    public Dialogue(Dialogue dialogue)
    {
        this.id = dialogue.id;
        this.dialogueString = dialogue.dialogueString;
        this.hasQuestDialogue = dialogue.hasQuestDialogue;
        this.questDialogue = dialogue.questDialogue;
    }
}
