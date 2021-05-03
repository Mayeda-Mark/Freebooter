using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Response Quest", menuName = "Quest Responses")]
public class QuestResponse : PlayerResponse
{
    public bool startingQuest = default;
    public int questId = default;
    public Quest quest;
    public QuestItem questItem;

    public override void ResponseButton()
    {
        if(startingQuest)
        {
            quest.GiveQuest();
            FindObjectOfType<DialogueController>().CloseDialogue();
        }
        else
        {
            quest.AdvanceQuest();
        }
    }
}
