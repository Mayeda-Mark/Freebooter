using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest Item", menuName = "Quest Items")]
public class QuestItem: ScriptableObject
{
    [System.Serializable]
    public class QuestItemReward
    {
        public string name;
        public int itemId;
        public int quantity;
    }
    public int parentId;
    public int questStep;
    //public string name;
    public string description;
    public List<QuestItemReward> rewards = new List<QuestItemReward>();
    [HideInInspector] public bool completed = false;

    public void CompleteQuestItem()
    {
        completed = true;
        FindObjectOfType<GameController>().AdvanceThisQuest(parentId, questStep);
        foreach(QuestItemReward reward in rewards)
        {
            FindObjectOfType<Inventory>().GiveItem(reward.itemId, reward.quantity);
        }
    }
}
