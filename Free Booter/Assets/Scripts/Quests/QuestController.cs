using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    private QuestDB db;
    public List<Quest> playerQuests = new List<Quest>();
    // UI STUFFS

    private void Start()
    {
        db = GetComponent<QuestDB>();
    }
    public void GiveQuest(/*Quest quest*/int id)
    {
        Quest questInInventory = playerQuests.Find(playerQuest => playerQuest.id == id);
        if(questInInventory != null)
        {
            return;
        }
        else
        {
            Quest questToGive = db.GetQuest(id);
            playerQuests.Add(questToGive);
            print("Giving quest: " + playerQuests[0].title);
        }
    }
}
