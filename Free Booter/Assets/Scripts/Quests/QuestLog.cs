using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestLog : MonoBehaviour
{
    private Image checkMark;
    private Text questText;
    private Quest quest;
    private QuestItem questItem;
    public void ActivateSlot(Quest quest)
    {
        checkMark.gameObject.SetActive(quest.completed);
        string titleAndDescription = "" + quest.title + "\n \t" + quest.description;
        questText.text = titleAndDescription.ToString();
    }
    public void ActivateSlot(QuestItem questItem)
    {
        checkMark.gameObject.SetActive(questItem.completed);
        questText.text = questItem.description.ToString();
    }
}
