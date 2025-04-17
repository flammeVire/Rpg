using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BattleUI_Manager : MonoBehaviour
{
    public TextMeshProUGUI Name; 
    public TextMeshProUGUI Type; 
    public TextMeshProUGUI Damage; 
    public TextMeshProUGUI CurrentAmo; 
    public TextMeshProUGUI MaxTarget;

    public TextMeshProUGUI Allie1_Name;
    public TextMeshProUGUI Allie1_Pv;
    public TextMeshProUGUI Allie1_Def;
    public TextMeshProUGUI Allie2_Name;
    public TextMeshProUGUI Allie2_Pv;
    public TextMeshProUGUI Allie2_Def;
    public TextMeshProUGUI Allie3_Name;
    public TextMeshProUGUI Allie3_Pv;
    public TextMeshProUGUI Allie3_Def;

    public TextMeshProUGUI Ennemi1_Name;
    public TextMeshProUGUI Ennemi1_Pv;
    public TextMeshProUGUI Ennemi1_Def;
    public TextMeshProUGUI Ennemi2_Name;
    public TextMeshProUGUI Ennemi2_Pv;
    public TextMeshProUGUI Ennemi2_Def;
    public TextMeshProUGUI Ennemi3_Name;
    public TextMeshProUGUI Ennemi3_Pv;
    public TextMeshProUGUI Ennemi3_Def;

    public TextMeshProUGUI CurrentPlayer;
    public TextMeshProUGUI NextPlayer;
    public TextMeshProUGUI NextPlayer1;
    public void ChangeSpell(Attaque attaque, string NameOfAtk)
    {
        Name.text = NameOfAtk;
        Type.text = "Type:" + attaque.attack.ToString();
        Damage.text = "Damage: " + attaque.DamagePerAttack.ToString();
        CurrentAmo.text = "Current Ammo: " + attaque.currentBullet.ToString() + "/" + attaque.maxBullet.ToString();
        MaxTarget.text = "MaxTarget: " + attaque.NumberOfTarget;
    }

    public void ClearSpell()
    {
        Name.text = "Select a Spell";
        Type.text = "Type:";
        Damage.text = "Damage: ";
        CurrentAmo.text = "Current Ammo: ";
        MaxTarget.text = "MaxTarget: ";
    }


    public void UpdatePv(Pnj_Data Data,bool IsAllie,int Index)
    {

        switch (Index)
        {
            case 0:
                if (IsAllie)
                {
                    Allie1_Name.text = Data.Name;
                    Allie1_Pv.text = "pv: "  +Data.CurrentPV.ToString();
                    Allie1_Def.text = "Def: "+Data.Def.ToString();
                }
                else
                {
                    Ennemi1_Name.text = Data.Name;
                    Ennemi1_Pv.text = "pv: " + Data.CurrentPV.ToString();
                    Ennemi1_Def.text = "Def: "+ Data.Def.ToString();
                }
                break;
            case 1:
                if (IsAllie)
                {
                    Allie2_Name.text = Data.Name;
                    Allie2_Pv.text = "pv: " + Data.CurrentPV.ToString();
                    Allie2_Def.text = "Def: "+ Data.Def.ToString();
                }
                else
                {
                    Ennemi2_Name.text = Data.Name;
                    Ennemi2_Pv.text = "pv: " + Data.CurrentPV.ToString();
                    Ennemi2_Def.text = "Def: "+ Data.Def.ToString();
                }
                break;
            case 2:
                if (IsAllie)
                {
                    Allie3_Name.text = Data.Name;
                    Allie3_Pv.text = "pv: " + Data.CurrentPV.ToString();
                    Allie3_Def.text = "Def: "+ Data.Def.ToString();
                }
                else
                {
                    Ennemi3_Name.text = Data.Name;
                    Ennemi3_Pv.text = "pv: " + Data.CurrentPV.ToString();
                    Ennemi3_Def.text = "Def: "+ Data.Def.ToString();
                }
                break;
        }
    }

    public void ManageTurn(string currentPlayer,string nextPlayer,string nextPlayer1)
    {
        CurrentPlayer.text = currentPlayer;
        NextPlayer.text = nextPlayer;
        NextPlayer1.text = nextPlayer1;
    }
}
