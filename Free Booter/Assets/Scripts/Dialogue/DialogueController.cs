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
        List<PlayerResponse> activeResponses = new List<PlayerResponse>();
        for(int i = 0; i < dialogue.playerResponses.Length; i++)
        {
            activeResponses.Add(dialogue.playerResponses[i]);
        }
        foreach(PlayerResponse playerResponse in dialogue.playerResponses)
        {
            QuestResponse questResponse;
            if (playerResponse.GetType() == typeof(QuestResponse))
            {
                questResponse = (QuestResponse)playerResponse;
            }
            else { return; }
            //int questId = questResponse.quest.id;
            Quest foundPlayerQuest = FindObjectOfType<QuestController>().FindQuest(questResponse.quest.id);/*playerQuests.Find(playerQuest => playerQuest.id == questResponse.quest.id);*/
            if(foundPlayerQuest != null)
            {
                if (questResponse.startingQuest)
                { //CHECKING QUEST
                    activeResponses.Remove(playerResponse);
                }
                else if(!questResponse.startingQuest)
                { // CHECKING QUEST ITEM
                    if(foundPlayerQuest.activeStep > questResponse.questStep)
                    {
                        activeResponses.Remove(playerResponse);
                    }
                }
            }
        }
        for(int i = 0; i < /*dialogue.playerResponses.Length*/activeResponses.Count; i++)
        {
            playerResponseButtons[i].gameObject.SetActive(true);
            buttonTexts[i].text = /*dialogue.playerResponses*/activeResponses[i].responseText.ToString();
            ChangeButtonOnClick(i, /*dialogue.playerResponses*/activeResponses[i]);
        }
        if(playerResponseButtons.Length > /*dialogue.playerResponses.Length*/activeResponses.Count)
        {
            for(int i = dialogue.playerResponses.Length; i < /*playerResponseButtons.Length*/activeResponses.Count; i++)
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
