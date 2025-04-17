using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static Attaque;

public class BattleManagement : MonoBehaviour
{
    bool IsAttackSelected;
    Attaque AttaqueSelected;
    bool IsTargetSelected;
    Pnj_Data Target;

    public BattleUI_Manager battleUI_Manager;

    public Pnj_Data[] allPnj = new Pnj_Data[6];
    List<Pnj_Data> EnnemisList = new List<Pnj_Data>();
    List<Pnj_Data> AllieList = new List<Pnj_Data>();

    int ResetSpeed;
    int Turn;
    Pnj_Data myself;

    bool isCombatEnd = false;

    [SerializeField] GameObject[] Spawner;
    [SerializeField] GameObject Battle_Ennemis;


    private void Start()
    {
        GetFighter();
        InitMesh();
        StartCoroutine(Player_Turn());
        GetAllFighter();
    }
    private void Update()
    {
        SelectedEnnemis();
        EndCombat();
        Updateui();
    }

    IEnumerator Player_Turn()
    {
        yield return null;
        myself = OrderForCombat();
        Debug.LogWarning(myself);
        Debug.LogWarning(myself.Speed);
        if (AllieList.Contains(myself))
        {

            yield return new WaitUntil(() => IsAttackSelected);
            Debug.Log("attaque selected");
            yield return new WaitUntil(() => IsTargetSelected);
            Debug.Log("target selected");
            Attack();
            CheckPv(Target);
            IsAttackSelected = false;
            AttaqueSelected = null;
            IsTargetSelected = false;
            Target = null;
            Debug.Log("fin attaque");
        }
        else
        {

            StartCoroutine(Ennemis_Turn());
        }
        myself.Speed *= -1;
        ResetSpeed += 1;
        if (ResetSpeed == allPnj.Length)
        {
            ResetSpeed = 0;
            for (int i = 0; i < allPnj.Length; i++)
            {
                allPnj[i].Speed *= -1;
            }
        }
        Debug.LogWarning("Speed = " + myself.Speed);
        StartCoroutine(Player_Turn());
    }
    IEnumerator Ennemis_Turn()
    {
        yield return null;
        Debug.Log("tour de l'ennemis");
        int random = UnityEngine.Random.Range(0, 3);
        if (random == 0)
        {
            AttaqueSelected = myself.FirstAttack;
        }
        else if (random == 1)
        {
            AttaqueSelected = myself.SecondAttack;
        }
        else if (random == 2)
        {
            AttaqueSelected = myself.ThirdAttack;
        }

        int randomtarget = UnityEngine.Random.Range(0, AllieList.Count);
        if (randomtarget == 0)
        {
            Target = AllieList[randomtarget];
        }
        else if (randomtarget == 1)
        {
            Target = AllieList[randomtarget];
        }
        else if (randomtarget == 2)
        {
            Target = AllieList[randomtarget];
        }
        Target = AllieList[0];
        Attack();
        CheckPv(Target);
        battleUI_Manager.ClearSpell();
    }

    public void SelectedEnnemis()
    {
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit raycastHit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out raycastHit, 100f))
            {
                if (raycastHit.transform != null)
                {
                    GameObject obj = raycastHit.transform.gameObject;
                    if (obj.tag == "ennemis")
                    {

                        Debug.Log("aaaaaaaaaaaa");
                        GetCurrentTarget(obj);
                        IsTargetSelected = true;
                    }
                }
            }
        }
    }

    void GetCurrentTarget(GameObject obj)
    {
        for (int i = 0; i < Spawner.Length; i++)
        {
            GameObject parent1 = obj.transform.parent.gameObject;
            GameObject parent2 = parent1.transform.parent.gameObject;
            if (parent2 == Spawner[i].gameObject)
            {
                Target = EnnemisList[i];
                Debug.Log("EnnemisTrouvéName " + Target.Name);
            }
        }
    }

    #region button 
    public void Spell1()
    {
        AttaqueSelected = myself.FirstAttack;
        IsAttackSelected = true;
        battleUI_Manager.ChangeSpell(AttaqueSelected, "Spell1");
    }

    public void Spell2()
    {
        AttaqueSelected = myself.SecondAttack;
        IsAttackSelected = true;
        battleUI_Manager.ChangeSpell(AttaqueSelected, "Spell2");
    }

    public void Spell3()
    {
        AttaqueSelected = myself.ThirdAttack;
        IsAttackSelected = true;
        battleUI_Manager.ChangeSpell(AttaqueSelected, "Spell3");
    }
    #endregion

    #region attaque

    void Attack()
    {
        //attaque selected = attaque(on connais déjà, ennemi on connais, savoir type de l'attaque et euhhh aussi euhh rajouter le nombre de target(attaque de zone/attaque ciblée).
        if (AttaqueSelected.attack == Attaque.TypeOfAttack.Spell)
        {
            Spell(AttaqueSelected, Target);
        }
        else if (AttaqueSelected.attack == Attaque.TypeOfAttack.Range)
        {
            RangeAttack(AttaqueSelected, Target);
        }
        else if (AttaqueSelected.attack == Attaque.TypeOfAttack.Close)
        {
            CloseAttack(AttaqueSelected, Target);
        }
;
    }

    void CloseAttack(Attaque me, Pnj_Data target)
    {
        Debug.Log("close attack");
        if (target.Def > 0)
        {
            Debug.Log("target def == " + target.Def);
            target.Def -= me.DamagePerAttack;
            Debug.Log("target def == " + target.Def);
        }
        else
        {
            Debug.Log("target pv == " + target.CurrentPV);
            target.CurrentPV -= me.DamagePerAttack;
            Debug.Log("target pv == " + target.CurrentPV);
        }
    }

    void RangeAttack(Attaque me, Pnj_Data target)
    {
        Debug.Log("Range attack");
        for (int i = 0; i < me.NumberBulletShoot; i++)
        {
            if (me.currentBullet > 0)
            {
                if (target.Def > 0)
                {
                    Debug.Log("target def == " + target.Def);
                    target.Def -= me.DamagePerAttack;
                    Debug.Log("target def == " + target.Def);
                }
                else
                {
                    Debug.Log("target pv == " + target.CurrentPV);
                    target.CurrentPV -= me.DamagePerAttack;
                    Debug.Log("target pv == " + target.CurrentPV);
                }
                me.currentBullet--;
            }
            else
            {
                me.currentBullet = me.maxBullet;
                break;
            }
        }
    }

    void Spell(Attaque me, Pnj_Data target)
    {
        /*
        switch (me.effect)
        {
            case Attaque.Stat_Effect.Speed:

                target.Speed += me.IsTargetAllies ? me.Modifier : -me.Modifier;
                break;
            case Attaque.Stat_Effect.Def:
                target.Def += me.IsTargetAllies ? me.Modifier : -me.Modifier;

                break;
            case Attaque.Stat_Effect.Bullet:
                ///
                break;
            default:
                Debug.LogError("Spell Not Found");
                break;
        }*/


        if (me.effect == Stat_Effect.Speed)
        {
            target.Speed += me.IsTargetAllies ? me.Modifier : -me.Modifier;
            Debug.Log("ca marche ou bien");
        }
        else if (me.effect == Stat_Effect.Def)
        {
            target.Def += me.IsTargetAllies ? me.Modifier : -me.Modifier;
            Debug.Log("ca marche ou bien mais pas pareil");
        }

    }

    #endregion

    #region BeforeCombat

    /// <summary>
    /// initalisez current pv
    /// </summary>

    void GetFighter()
    {

        for (int i = 0; i < GameManager.instance.HeroTeam.Length; i++)
        {
            if (GameManager.instance.HeroTeam[i] != null)
            {
                Debug.Log("adding hero" + GameManager.instance.HeroTeam[i]);
                AllieList.Add(GameManager.instance.HeroTeam[i]);
            }
        }
        for (int i = 0; i < GameManager.instance.nextEnnemis.Length; i++)
        {
            if (GameManager.instance.nextEnnemis[i] != null)
            {
                EnnemisList.Add(GameManager.instance.nextEnnemis[i]);
            }
        }
    }

    void InitMesh()
    {
        for (int i = 0; i < EnnemisList.Count && i < 3; i++)
        {
            if (EnnemisList[i] != null)
            {
                GameObject parent = Instantiate(Battle_Ennemis, Spawner[i].transform);
                GameObject child = Instantiate(EnnemisList[i].Mesh, Spawner[i].transform);
                child.tag = "ennemis";
                EnnemisList[i].ID = i;
                child.transform.SetParent(parent.transform);
                EnnemisList[i].Mesh = child;
            }
        }

        // Spawn Alliés
        for (int i = 0; i < AllieList.Count && i < 3; i++)
        {
            if (AllieList[i] != null)
            {
                GameObject parent = Instantiate(Battle_Ennemis, Spawner[i + 3].transform);
                GameObject child = Instantiate(AllieList[i].Mesh, Spawner[i + 3].transform);
                child.transform.SetParent(parent.transform);
            }
        }
    }

    #endregion

    void GetAllFighter()
    {
        allPnj = EnnemisList.Concat(AllieList).ToArray();
    }

    Pnj_Data OrderForCombat()
    {

        Pnj_Data pnjGoingToPlay = allPnj[0];
        Pnj_Data nextPlayerTurn = allPnj[0];
        for (int i = 0; i < allPnj.Length; i++)
        {
            if (allPnj[i] != null)
            {
                if (allPnj[i].Speed > pnjGoingToPlay.Speed)
                {
                    pnjGoingToPlay = allPnj[i];
                }
            }
            
        }


        for(int i = 0;i < allPnj.Length; i++)
        {
            if(allPnj[i] != null)
            {
                if (allPnj[i] != pnjGoingToPlay)
                {
                    if (allPnj[i].Speed > nextPlayerTurn.Speed)
                    {
                        nextPlayerTurn = allPnj[i];
                    }
                }
            }
        }
        Debug.Log("pnj going to play is: " + pnjGoingToPlay);
        battleUI_Manager.NextPlayer.text = nextPlayerTurn.Name;
        battleUI_Manager.CurrentPlayer.text = pnjGoingToPlay.Name;
        return pnjGoingToPlay;
    }

    void EndCombat()
    {
        if (AllieList.Count <= 0 || EnnemisList.Count <= 0)
        {
            if (!isCombatEnd)
            {
                StopAllCoroutines();
                StartCoroutine(WaitToEnd());
            }

            isCombatEnd = true;
        }
    }
    void kill(GameObject ennemis, Pnj_Data data)
    {
        Destroy(ennemis);
        if (EnnemisList.Contains(data))
        {
            EnnemisList.Remove(data);
        }
        else if (AllieList.Contains(data))
        {
            AllieList.Remove(data);
        }
        for (int i = 0; i < allPnj.Length; i++)
        {
            if (allPnj[i] != null && allPnj[i] == data)
            {
                allPnj[i] = null;
            }
        }
    }

    void CheckPv(Pnj_Data target)
    {

        if (target.CurrentPV <= 0)
        {
            kill(target.Mesh, target);
        }
    }

    IEnumerator WaitToEnd()
    {
        yield return new WaitForSeconds(3f);
        Debug.Log("AAAAAAAAAAAA");
        if (GameManager.instance.nextEnnemis.Length > 0)
        {
            for (int i = 0; i < GameManager.instance.nextEnnemis.Length; i++)
            {
                GameManager.instance.nextEnnemis[i] = null;
            }
        }
        GameManager.instance.ReturnPlayerAtOldScene();

    }

    void Updateui()
    {
        for (int i = 0; i < AllieList.Count; i++)
        {
            battleUI_Manager.UpdatePv(AllieList[i],true,i);
        }
        for (int i = 0; i < EnnemisList.Count; i++)
        {
            battleUI_Manager.UpdatePv(EnnemisList[i],false,i);
        }
    }
}
