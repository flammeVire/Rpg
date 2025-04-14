using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime.Misc;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Scriptable/Quest", order = 1)]
public class Quest : ScriptableObject
{
    public TypeOfQuest type;
    public int questID;
    [SerializeField] public string questName;
    [SerializeField] public Quest nextQuest; 
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

    public void ResetQuest()
    {
        Finish = new bool[Step.Length];
        CurrentStep = 0;
    }

    public bool IsCompleted()
    {
        foreach (bool step in Finish)
        {
            if (!step) return false;
        }
        return true;
    }

    public void MarkStepAsDone(string targetName)
    {
        if (Step != null && CurrentStep < Step.Length)
        {
            if (Step[CurrentStep].NameOfTarget == targetName)
            {
                Finish[CurrentStep] = true;
                CurrentStep++;
            }
        }
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