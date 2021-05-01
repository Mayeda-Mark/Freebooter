using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDb : MonoBehaviour
{
    [SerializeField] public List<Quest> quests = new List<Quest>();
    public QuestItem[] questItems;
    void Start()
    {
        BuildDb();
        int numDBs = FindObjectsOfType<QuestDb>().Length;
        if (numDBs > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public Quest GetQuest(int id)
    {
        return quests.Find(quest => quest.id == id);
    }
    private void BuildDb()
    {
        quests = new List<Quest>
        {
            new Quest(0, "Tutorial", "Learn the basics of Freebooter", 
            new List<QuestItem>
            {
                questItems[0],
                questItems[1],
                questItems[2]
            },
            new Dictionary<int, int>
            { { 2, 100 } }),
        };
    }
}
