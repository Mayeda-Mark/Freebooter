using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueController : MonoBehaviour
{
    public GameObject dialogueCanvas;
    public Text dialogueText;
    private DialogueDB db;
    private void Start()
    {
        db = GetComponent<DialogueDB>();
        dialogueCanvas.SetActive(false);
    }
    public void ActivateDialogue(int dialogueId)
    {
        Time.timeScale = 0;
        dialogueCanvas.SetActive(true);
        dialogueText.text = db.GetDialogue(dialogueId).dialogueString.ToString();
    }
    public void CloseDialogue()
    {
        Time.timeScale = 1;
        dialogueCanvas.SetActive(false);
    }
}
