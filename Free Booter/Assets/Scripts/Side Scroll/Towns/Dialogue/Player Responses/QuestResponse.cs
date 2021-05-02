using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Response Quest", menuName = "Quest Responses")]
public class QuestResponse : PlayerResponse
{
    public bool startingQuest = default;
    public int questId = default;
    public QuestItem questItem;

    public override void ResponseButton()
    {
        if(startingQuest)
        {
            // START THE QUEST
        }
        else
        {
            // ADVANCE THE QUEST
        }
    }
}
