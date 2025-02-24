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


    public Pnj_Data[] allPnj = new Pnj_Data[6];
    List<Pnj_Data> EnnemisList = new List<Pnj_Data>();
    List<Pnj_Data> AllieList = new List<Pnj_Data>();


    Pnj_Data myself;

    [SerializeField] GameObject[] Spawner;
    [SerializeField] GameObject Battle_Ennemis;


    private void Start()
    {
        GetFighter();
        InitMesh();
        StartCoroutine(Player_Turn());
    }
    private void Update()
    {
        SelectedEnnemis();
        EndCombat();
    }

    IEnumerator Player_Turn()
    {
        yield return null;
        myself = OrderForCombat();
        if (AllieList.Contains(myself))
        {

            yield return new WaitUntil(() => IsAttackSelected);
            Debug.Log("attaque selected");
            yield return new WaitUntil(() => IsTargetSelected);
            Debug.Log("target selected");
            Attack();

            IsAttackSelected = false;
            AttaqueSelected = null;
            IsTargetSelected = false;
            Target = null;
            Debug.Log("fin attaque");
        }
        else
        {
            IATurn();///
        }
        myself.Speed *= -1;
        StartCoroutine(Player_Turn());
    }
    private void IATurn()
    {
        
    }
    IEnumerator Ennemis_Turn()
    {
        yield return null;
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
            if(parent2 == Spawner[i].gameObject)
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
    }

    public void Spell2()
    {
        AttaqueSelected = myself.SecondAttack;
        IsAttackSelected = true;
    }

    public void Spell3()
    {
        AttaqueSelected = myself.ThirdAttack;
        IsAttackSelected = true;
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
            Debug.Log("target def == " +  target.Def);
            target.Def -= me.DamagePerAttack;
            Debug.Log("target def == " +  target.Def);
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

    

    Pnj_Data OrderForCombat()
    {
        allPnj = new Pnj_Data[EnnemisList.Count + AllieList.Count];
        EnnemisList.CopyTo(allPnj, 0);
        AllieList.CopyTo(allPnj, EnnemisList.Count);

        allPnj.OrderByDescending(pnj => pnj.Speed).ToArray();
        Debug.Log("pnj 0 = " + allPnj[0]);
        System.Array.Reverse(allPnj);
        Debug.Log("pnj 0 = " + allPnj[0]);
        return allPnj[0];
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
