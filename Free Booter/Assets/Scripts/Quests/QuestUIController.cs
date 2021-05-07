using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUIController : MonoBehaviour
{
    public GameObject prefab, questItemPrefab = default;
   //ublic GameObject questPanel = default;
    private QuestController questController;
    void Start()
    {
        questController = FindObjectOfType<QuestController>();
    }
    public void ActivateUI()
    {
        List<Quest> quests = questController.playerQuests;
        for(int i = 0; i < transform.childCount; i ++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        foreach(Quest playerQuest in quests)
        {
            if(!playerQuest.completed)
            {
                AttachUI(playerQuest);
                /*foreach(QuestItem questItem in playerQuest.questItems)
                {
                    AttachQuestItem(questItem);
                }*/
            }
            else
            {
                AttachUI(playerQuest);
                /*foreach (QuestItem questItem in playerQuest.questItems)
                {
                    AttachQuestItem(questItem);
                }*/
            }
        }
    }
    private void AttachUI(Quest quest)
    {
        GameObject instance = Instantiate(prefab);
        instance.GetComponent<QuestLog>().ActivateSlot(quest);
        instance.transform.parent = this.transform;
        /*foreach(QuestItem questItem in quest.questItems)
        {
            AttachQuestItem(questItem, instance.GetComponent<QuestLog>());
        }*/
        for(int i = 0; i <= quest.activeStep; i++)
        {
            AttachQuestItem(quest.questItems[i], instance.GetComponent<QuestLog>());
        }
    }
    private void AttachQuestItem(QuestItem questItem, QuestLog parentQuest)
    {
        GameObject instance = Instantiate(questItemPrefab);
        instance.GetComponent<QuestLog>().ActivateQuestItemSlot(questItem);
        instance.transform.parent = parentQuest.grid.transform;
    }
}
