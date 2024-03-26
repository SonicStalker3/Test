using System.Collections;
using System.Collections.Generic;
using Scriptable;
using Scriptable.Dialog;
using UnityEngine;

[CreateAssetMenu(menuName = "Quests/Quest", order = -1)]
public class QuestObject : ScriptableObject 
{
    public string Header;
    public string Description;
    public DialogObject preQuestDialog;
    public DialogObject postQuestDialog;
    public Requirement[] requirements; 
    public enum QuestState { NotStarted, InProgress, Completed, Failed }
    public QuestState questState;

    public DialogObject StartMission()
    {
        questState = QuestState.InProgress;
        return preQuestDialog;
    }

    public DialogObject Complete() 
    {
        questState = QuestState.Completed;
        return postQuestDialog;
    }
}
[System.Serializable]

public class Requirement
{
    public RequirementType type;
    //public string description;

    public ItemHandler itemReference;
    [Range(1, 50)]
    public int itemValues = 1;
    
    public NPC npcReference;
    public DialogObject npcValues;
    
    public string levelReference;
    
    /*public Vector3 vipReference;
    public DialogObject vipValues;*/
}

public enum RequirementType
{
    [InspectorName("Collect Item")]
    Item = 0,
    [InspectorName("Lead to the point of rescue")]
    Npc,
    Vip
}
