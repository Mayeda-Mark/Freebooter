using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueDB : MonoBehaviour
{
    public List<Dialogue> dialogues = new List<Dialogue>();

    private void Awake()
    {
        BuildDb();
        int numDBs = FindObjectsOfType<DialogueDB>().Length;
        if (numDBs > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }
    public Dialogue GetDialogue(int id)
    {
        return dialogues.Find(dialogue => dialogue.id == id);
    }
    void BuildDb()
    {
        dialogues = new List<Dialogue>
        {
            new Dialogue(0, "Testing1", false, 
            new Dictionary<int, string>
            { {0, "" } }),
        };
    }
}
