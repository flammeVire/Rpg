using System;
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
        if (!AlreadyGiveQuest)
        {
            Debug.Log($"Donne la quête : {quest.questName}");
            PlayerQuestManagament.instance.AddQuest(quest);
            AlreadyGiveQuest = true;
        }
        else if (!QuestReturned && PlayerQuestManagament.instance.IsQuestCompleted(quest))
        {
            Debug.Log($"Quête terminée : {quest.questName}");
            GiveReward();
            PlayerQuestManagament.instance.RemoveQuest(quest);
            QuestReturned = true;

            if (quest.nextQuest != null)
            {
                Debug.Log("Prochaine quête disponible !");
                quest = quest.nextQuest;
                AlreadyGiveQuest = false;
                QuestReturned = false;
            }
            else
            {
                Debug.Log("Fin de la chaîne de quêtes.");
            }
        }
        else
        {
            Debug.Log("Tu dois terminer la quête actuelle !");
        }
    }

    void GiveReward()
    {
        Debug.Log("Récompense : 100 gold (test)");
    }
}

