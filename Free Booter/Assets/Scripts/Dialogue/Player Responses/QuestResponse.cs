using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Response-Quest", menuName = "Response-Quest ")]
public class QuestResponse : PlayerResponse
{
    public bool startingQuest = default;
    public Quest quest;
    public int questStep = default;
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
            if(questStep == quest.activeStep)
            {
                quest.AdvanceQuest();
                FindObjectOfType<DialogueController>().CloseDialogue();
            }
        }
    }
}
