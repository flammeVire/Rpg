using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PnjQuest : MonoBehaviour
{
    [SerializeField] Quest quest;
    [SerializeField] bool AlreadyGiveQuest;
    public bool QuestReturned;

    // ajouter reward
    public void ManageQuest()
    {
        /*
    if (!QuestReturned)
    {
        if (!AlreadyGiveQuest)
        {
            //donne la qu�te
            Debug.Log("giveQuest");
            return quest;
        }
        else
        {
            bool QuestFinish = true;
            foreach (bool step in quest.Finish)
            {
                if (!step)
                {
                    QuestFinish = false;
                    break;
                }
            }
            if (QuestFinish)
            {
                QuestReturned = true;
                Debug.Log("questFinish");
            }
        }
    }
        */

        Debug.Log("Giving Quest");
        if (!AlreadyGiveQuest)
        {
            PlayerQuestManagament.instance.AddQuest(quest);
            AlreadyGiveQuest = true;
        }
        else
        {
            bool allQuestFinish = true;
            foreach (bool step in quest.Finish)
            {
                if (!step)
                {
                    allQuestFinish = false;
                    break;
                }
            }

            if (allQuestFinish)
            {
                PlayerQuestManagament.instance.RemoveQuest(quest);
                QuestReturned = true;
                /// mettre ici la recompense de la quête
            }
        }
    }
}
