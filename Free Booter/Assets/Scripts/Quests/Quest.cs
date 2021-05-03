using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Quests")]
public class Quest : ScriptableObject
{
    public int id;
    public string title;
    public string description;
    public int activeStep = 0;
    public List<QuestItem> questItems = new List<QuestItem>();
    public Dictionary<int, int> reward = new Dictionary<int, int>();
    public bool completed = false;
    public void AdvanceQuest()
    {
        activeStep ++;
        if(activeStep >= questItems.Count - 1)
        {
            completed = true;
        }
    }
    public void GiveQuest()
    {
        FindObjectOfType<QuestController>().GiveQuest(id);
    }
}
