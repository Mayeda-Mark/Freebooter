using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUIController : MonoBehaviour
{
    public GameObject prefab = default;
    public GameObject questPanel = default;
    private QuestController questController;
    void Start()
    {
        questController = FindObjectOfType<QuestController>();
    }
    public void ActivateUI()
    {
        List<Quest> quests = questController.playerQuests;
        foreach(Quest playerQuest in quests)
        {
            if(!playerQuest.completed)
            {
                AttachUI(playerQuest);
            }
            else
            {
                AttachUI(playerQuest);
            }
        }
    }
    private void AttachUI(Quest quest)
    {
        GameObject instance = Instantiate(prefab);
        instance.GetComponent<QuestLog>().ActivateSlot(quest);
    }
    private void AttachQuestItem(QuestItem questItem)
    {

    }
}
