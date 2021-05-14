using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    private Inventory inventory;
    private QuestLog questLog;
    private List<Quest> playerQuests = new List<Quest>();
    void Start()
    {
        if(FindObjectsOfType<GameController>().Length > 1)
        {
            Destroy(gameObject);
        } else
        {
            DontDestroyOnLoad(this);
        }
        inventory = FindObjectOfType<Inventory>();
    }
    public void GiveStartingInventory()
    {
        inventory.GiveItem(1, 80);
        inventory.GiveItem(2, 50);
        inventory.GiveItem(0, 100);
        inventory.GiveItem(8, 1);
        inventory.GiveItem(6, 1);
    }
    public void AdvanceThisQuest(int questId, int stepIndex)
    {
        Quest quest = FindPlayerQuest(questId);
        if(quest != null && quest.activeStep == stepIndex)
        {
            quest.AdvanceQuest();
        }
    }
    private Quest FindPlayerQuest(int id)
    {
        return playerQuests.Find(quest => quest.id == id);
    }
}
