using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Response Branch", menuName = "Branch Responses")]
public class BranchResponse : PlayerResponse
{
    public NPCDialogue[] nextSteps;

    public override void ResponseButton()
    {
        FindObjectOfType<DialogueController>().ActivateDialogue(nextSteps[(int)Random.Range(0, nextSteps.Length - 1)]);
    }
}
