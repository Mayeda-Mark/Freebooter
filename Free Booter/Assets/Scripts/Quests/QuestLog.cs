using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{
    public Image checkMark;
    public Text questText, descriptionText;
    public GameObject grid = default;
    private Quest quest;
    private QuestItem questItem;
    public void ActivateSlot(Quest quest)
    {
        checkMark.gameObject.SetActive(quest.completed);
        //string titleAndDescription = "" + quest.title + "\n \t" + quest.description;
        questText.text = quest.title.ToString();//titleAndDescription.ToString();
        descriptionText.text = quest.description;
    }
    public void ActivateQuestItemSlot(QuestItem questItem)
    {
        checkMark.gameObject.SetActive(questItem.completed);
        questText.text = questItem.description.ToString();
    }
}
