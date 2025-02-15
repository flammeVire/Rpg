using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleManagement : MonoBehaviour
{
    bool Spell1Active;
    bool Spell2Active;
    bool Spell3Active;

    List<Pnj_Data> EnnemisList;
    List<Pnj_Data> AllieList;

    Pnj_Data ennemiSelected;
    Pnj_Data allieSelected;


    private void Start()
    {
        StartCoroutine(Player_Turn());
    }

    IEnumerator Player_Turn()
    {
        yield return null;
    }
    IEnumerator Ennemis_Turn()
    {
        yield return null;
    }

    #region button 
    public void SelectedEnnemis(Pnj_Data pnj)
    {
        // si un spell et activé fait un truc
        if(Spell1Active || Spell2Active || Spell3Active) 
        {
            
        }
    }
    
    public void Spell1()
    {
        Spell1Active = true;
        Spell2Active = false;
        Spell3Active = false;
    }

    public void Spell2()
    {
        Spell1Active = false;
        Spell2Active = true;
        Spell3Active = false;
    }

    public void Spell3() 
    {
        Spell1Active = false;
        Spell2Active = false;
        Spell3Active = true;
    }
    #endregion


    #region attaque

    void Attaque1()
    {

    }

    #endregion
    private void Update()
    {
        EndCombat();
    }
    void EndCombat()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            GameManager.instance.ReturnPlayerAtOldScene();
            for (int i = 0; i < GameManager.instance.nextEnnemis.Length; i++) 
            {
                GameManager.instance.nextEnnemis[i] = null;
            }
        }
    }
}
