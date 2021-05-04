using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestUIController : MonoBehaviour
{
    public GameObject prefab = default;
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
        instance.transform.parent = this.transform;
    }
    private void AttachQuestItem(QuestItem questItem)
    {

    }
}
