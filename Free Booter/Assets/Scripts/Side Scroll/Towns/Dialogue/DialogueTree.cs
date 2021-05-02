using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "New Dialogue Tree", menuName = "Dialogue Trees")]
public class DialogueTree  : MonoBehaviour/*: ScriptableObject*/
{
    public NPCDialogue[] nPCDialogues;

    public void StartDialogue()
    {
        FindObjectOfType<DialogueController>().ActivateDialogue(nPCDialogues[(int)Random.Range(0, nPCDialogues.Length - 1)]);
    }
}
