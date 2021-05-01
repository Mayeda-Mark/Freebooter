using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest
{
    public int id;
    public string title;
    public string description;
    //public bool unlocked;
    public int activeStep = 0;
    public List<QuestItem> questItems = new List<QuestItem>();
    public Dictionary<int, int> reward = new Dictionary<int, int>();
    public bool completed = false;
    
    public Quest(int id, string title, string description/*, int activeStep*/, List<QuestItem> questItems, Dictionary<int, int> reward/*, bool completed*/)
    {
        this.id = id;
        this.title = title;
        this.description = description;
        this.questItems = questItems;
        this.reward = reward;
        //this.completed = completed;
    }
    public Quest(Quest quest)
    {
        id = quest.id;
        title = quest.title;
        description = quest.description;
        questItems = quest.questItems;
        reward = quest.reward;
        //completed = quest.completed;
    }
    public void AdvanceQuest()
    {
        activeStep ++;
        if(activeStep >= questItems.Count - 1)
        {
            completed = true;
        }
    }
}
