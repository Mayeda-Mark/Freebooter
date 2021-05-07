using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Response Close", menuName = "Close Responses")]
public class CloseResponse : PlayerResponse
{
    public override void ResponseButton()
    {
        FindObjectOfType<DialogueController>().CloseDialogue();
    }
}
