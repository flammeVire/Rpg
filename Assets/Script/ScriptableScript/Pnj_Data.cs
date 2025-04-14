using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Scriptable/Data", order = 1)]
public class Pnj_Data : ScriptableObject
{
    public string Name;
    [HideInInspector]public int ID;
    public GameObject Mesh;
    public float PV;
    public float CurrentPV;
    public float Def;
    public float Speed = 3;

    public Attaque FirstAttack;
    public Attaque SecondAttack;
    public Attaque ThirdAttack;

}

[System.Serializable]
public class Attaque
{
    public TypeOfAttack attack;

    public int DamagePerAttack;
    [Range(1,3)]public int NumberOfTarget;

    [Header("Range")]
    [SerializeField] public int maxBullet;
    [SerializeField] public int NumberBulletShoot;
    [SerializeField] public int currentBullet;

    [Header("Spell")]
    [SerializeField] public bool IsTargetAllies;
    [SerializeField] public Stat_Effect effect;
    public int Modifier;


    public enum Stat_Effect
    {
        None,
        Def,
        Speed,
        Bullet,
    }

    public enum TypeOfAttack
    {
        Range,
        Close,
        Spell,
    }

}
