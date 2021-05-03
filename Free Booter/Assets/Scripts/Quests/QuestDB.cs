using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestDB : MonoBehaviour
{
    [SerializeField] public List<Quest> quests = new List<Quest>();
    public QuestItem[] questItems;
    void Start()
    {
        int numDBs = FindObjectsOfType<QuestDB>().Length;
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
}
