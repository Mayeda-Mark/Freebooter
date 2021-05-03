using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class DialogueController : MonoBehaviour
{
    public GameObject dialogueCanvas;
    public Text dialogueText;
    private DialogueDB db;
    public Button[] playerResponseButtons;
    public Text[] buttonTexts;
    UnityEvent buttonListener = new UnityEvent();
    private void Start()
    {
        db = GetComponent<DialogueDB>();
        dialogueCanvas.SetActive(false);
        //buttonListener.AddListener(Test);
        //playerResponseButtons[0].onClick.AddListener(Test);
    }
    public void ActivateDialogue(NPCDialogue dialogue)
    {
        Time.timeScale = 0;
        dialogueCanvas.SetActive(true);
        dialogueText.text = dialogue.dialogue.ToString();
        for(int i = 0; i < dialogue.playerResponses.Length; i++)
        {
            playerResponseButtons[i].gameObject.SetActive(true);
            buttonTexts[i].text = dialogue.playerResponses[i].responseText.ToString();
            ChangeButtonOnClick(i, dialogue.playerResponses[i]);
        }
        if(playerResponseButtons.Length > dialogue.playerResponses.Length)
        {
            for(int i = dialogue.playerResponses.Length; i < playerResponseButtons.Length; i++)
            {
                playerResponseButtons[i].gameObject.SetActive(false);
            }
        }
    }
    private void ChangeButtonOnClick(int buttonIndex, PlayerResponse response)
    {
        playerResponseButtons[buttonIndex].onClick.RemoveAllListeners();
        playerResponseButtons[buttonIndex].onClick.AddListener(response.ResponseButton);
    }
    public void CloseDialogue()
    {
        Time.timeScale = 1;
        dialogueCanvas.SetActive(false);
    }
}
