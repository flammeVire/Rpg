using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Scriptable/Quest", order = 1)]
public class Quest : ScriptableObject
{
    public TypeOfQuest type;
    public int questID;
    [SerializeField] public string Name;
    [SerializeField] public int CurrentStep;
    [SerializeField] public StepQuest[] Step;
    [SerializeField] public string[] Description;
    [SerializeField] public bool[] Finish;

    public float Xp;
    public Pnj_Data AllieAdded;
    public enum TypeOfQuest
    {
        Kill,
        Talk,
        Pick,
        Other,
    }
}

[System.Serializable]
public class StepQuest
{
    public string NameOfTarget;
    [Header("Pick")]
    public int ItemID;

    [Header("KillQuest")]
    public int KillRequire;


}