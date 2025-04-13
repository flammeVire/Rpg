using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_Manager : MonoBehaviour
{
    public TextMeshProUGUI QuestName;
    public TextMeshProUGUI QuestDescription;


    public TextMeshProUGUI[] Heros_Name;



    void Update()
    {
        if (PlayerQuestManagament.instance.quests.Count > 0)
        {
            ShowQuest();
        }
        else
        {
            QuestName.text = "";
            QuestDescription.text = "";
        }
        ShowTeam();
    }


    void ShowQuest()
    {
        QuestName.text = PlayerQuestManagament.instance.quests[0].Name;
        QuestDescription.text = PlayerQuestManagament.instance.quests[0].Description[PlayerQuestManagament.instance.quests[0].CurrentStep];
    }

    void ShowTeam()
    {
        for (int i = 0; i < GameManager.instance.HeroTeam.Length; i++)
        {
            if(GameManager.instance.HeroTeam[i] != null)
            {
                Heros_Name[i].text = GameManager.instance.HeroTeam[i].Name;
            }
        }
    }
}
