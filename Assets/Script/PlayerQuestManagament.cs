using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestManagament : MonoBehaviour
{
    public static PlayerQuestManagament instance { get; private set; }
    public List<Quest> quests = new List<Quest>();
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
    }
    public void AddQuest(Quest quest)
    {
        if (quest != null)
        {
            quests.Add(quest);
        }
    }
    public void RemoveQuest(Quest quest)
    {
        if (quest != null)
        {
            quests.Remove(quest);
        }
    }

    private void Update()
    {
        for (int i = 0; i < quests.Count; i++)
        {
            if (quests[i] != null)
            {
                bool allFinish = true;
                foreach (bool finish in quests[i].Finish)
                {
                    if (!finish)
                    {
                        allFinish = false;
                        break;
                    }
                }

                if (allFinish)
                {
                    RemoveQuest(quests[i]);
                    break;
                }
            }
        }
    }
}