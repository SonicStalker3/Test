using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quests/Mission List", order = -1)]
public class StoryLine : ScriptableObject
{
    public QuestObject[] Quests;
}
