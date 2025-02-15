using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Quest", menuName = "Scriptable/Quest", order = 1)]
public class Quest : ScriptableObject
{
    public TypeOfQuest type;
    public int questID;
    [SerializeField] string Name;
    [SerializeField] int CurrentStep;
    [SerializeField] string[] Description;
    [SerializeField] public bool[] Finish;


    public enum TypeOfQuest
    {
        Kill,
        Talk,
        Pick,
        Other,
    }
}
