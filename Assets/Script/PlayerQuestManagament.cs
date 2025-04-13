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
        if (quest != null && !quests.Contains(quest))
        {
            quest.ResetQuest();
            quests.Add(quest);
            Debug.Log($"Quête '{quest.questName}' ajoutée.");
        }
    }

    public void CompleteStep(string targetName)
    {
        foreach (var quest in quests)
        {
            quest.MarkStepAsDone(targetName);
        }
    }

    public void RemoveQuest(Quest quest)
    {
        if (quests.Contains(quest))
        {
            quests.Remove(quest);
        }
    }

    public bool IsQuestCompleted(Quest quest)
    {
        return quest.IsCompleted();
    }

}
