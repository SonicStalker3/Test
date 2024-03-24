
using UnityEditor;
using UnityEngine;

/*
[CustomEditor(typeof(QuestObject))]
public class QuestObjectEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update(); // Обновляем данные объекта

        DrawDefaultInspector(); // Отображаем стандартный инспектор

        QuestObject questObject = (QuestObject)target;

        questObject.Header = EditorGUILayout.TextField("Header", questObject.Header);
        questObject.Description = EditorGUILayout.TextField("Description", questObject.Description);
        questObject.preQuestDialog = (DialogObject)EditorGUILayout.ObjectField("Pre-Quest Dialog", questObject.preQuestDialog, typeof(DialogObject), true);
        questObject.postQuestDialog = (DialogObject)EditorGUILayout.ObjectField("Post-Quest Dialog", questObject.postQuestDialog, typeof(DialogObject), true);
        // Отображаем поля Requirement
        //questObject.requirements.Length = EditorGUILayout.IntField(new GUIContent("Size"),questObject.requirements.Length);
        for (int i = 0; i < questObject.requirements.Length; i++)
        {
            //EditorGUI.DropdownButton("1");
            EditorGUILayout.LabelField($"Требование {i + 1}");

            // Создаем новый объект Requirement
            Requirement requirement = questObject.requirements[i];

            // Отображаем поля внутри объекта Requirement
            requirement.type = (RequirementType)EditorGUILayout.EnumPopup("Тип требования", requirement.type);
            requirement.description = EditorGUILayout.TextField("Описание требования", requirement.description);


            // В зависимости от типа требования, отображаем соответствующее поле
            switch (requirement.type)
            {
                case RequirementType.Item:
                    requirement.reference = EditorGUILayout.TextField("Reference", (string)requirement.reference);
                    //EditorGUILayout.PropertyField(requirements, new GUIContent($"Requirement {i + 1} Type"));// = EditorGUILayout.TextField("Item reference", (string)requirement.reference);
                    break;
                case RequirementType.NPC:
                    requirement.reference = EditorGUILayout.ObjectField("Reference", (NPC)requirement.reference, typeof(NPC), true);
                    break;
                case RequirementType.Level:
                    requirement.reference = EditorGUILayout.TextField("Reference", (string)requirement.reference);
                    break;
            }

            // Присваиваем обновленный объект Requirement обратно в массив
            questObject.requirements[i] = requirement;
        }
        questObject.questState = (QuestObject.QuestState)EditorGUILayout.EnumPopup("Quest State", questObject.questState);

        serializedObject.ApplyModifiedProperties();
    }
}*/

/*using System;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(QuestObject))]
public class QuestObjectEditor : Editor
{
    public bool isRequirementsShowed;
    public bool[] isRequirementsElementShowed;

    public override void OnInspectorGUI()
    {
        //DrawDefaultInspector();
        //DrawFoldoutInspector();
        serializedObject.Update(); // Обновляем данные объекта

        QuestObject questObject = (QuestObject)target;

        // Отображаем поля Header и Description
        questObject.Header = EditorGUILayout.TextField("Header", questObject.Header);
        questObject.Description = EditorGUILayout.TextField("Description", questObject.Description);

        // Отображаем поля preQuestDialog и postQuestDialog
        questObject.preQuestDialog = (DialogObject)EditorGUILayout.ObjectField("Pre-Quest Dialog",
            questObject.preQuestDialog, typeof(DialogObject), true);
        questObject.postQuestDialog = (DialogObject)EditorGUILayout.ObjectField("Post-Quest Dialog",
            questObject.postQuestDialog, typeof(DialogObject), true);

        // Отображаем поля Requirement в виде массива
        SerializedProperty requirementsArray = serializedObject.FindProperty("requirements");
        //Debug.Log(requirementsArray.FindPropertyRelative("Reference"));
        EditorGUILayout.PropertyField(requirementsArray, true);
        isRequirementsShowed = EditorGUILayout.Foldout(isRequirementsShowed, "Requirements");
        Debug.Log(isRequirementsShowed);
        // Отображаем поле questState
        if(isRequirementsShowed)
        {
            Array.Resize(ref isRequirementsElementShowed,requirementsArray.arraySize);
            for (int i = 0;i < requirementsArray.arraySize; i++) 
            {//foreach (SerializedProperty requirement in requirementsArray)
                GUILayout.Space(5);
                isRequirementsElementShowed[i] = EditorGUILayout.Foldout(isRequirementsElementShowed[i], $"Requirement {i}");
                if (isRequirementsElementShowed[i])
                { 
                    SerializedProperty typeProperty = requirementsArray.GetArrayElementAtIndex(i).FindPropertyRelative("type");
                    SerializedProperty descriptionProperty =
                    requirementsArray.GetArrayElementAtIndex(i).FindPropertyRelative("description"); 
                    SerializedProperty itemReferenceProperty =
                    requirementsArray.GetArrayElementAtIndex(i).FindPropertyRelative("itemReference"); 
                    SerializedProperty npcReferenceProperty =
                    requirementsArray.GetArrayElementAtIndex(i).FindPropertyRelative("npcReference");
                    SerializedProperty levelReferenceProperty =
                    requirementsArray.GetArrayElementAtIndex(i).FindPropertyRelative("levelReference");
                    
                    /*SerializedProperty typeProperty = requirement.FindPropertyRelative("type");
                    SerializedProperty itemReferenceProperty = requirement.FindPropertyRelative("itemReference");
                    SerializedProperty npcReferenceProperty = requirement.FindPropertyRelative("npcReference");
                    SerializedProperty levelReferenceProperty = requirement.FindPropertyRelative("levelReference");#1#

                    EditorGUILayout.LabelField(new GUIContent(i.ToString()));
                    EditorGUILayout.PropertyField(typeProperty);
                    EditorGUILayout.PropertyField(descriptionProperty);
                    switch ((RequirementType)typeProperty.enumValueIndex)
                    {
                        case RequirementType.Item:
                            EditorGUILayout.PropertyField(itemReferenceProperty, new GUIContent($"Reference {i}"));
                            break;
                        case RequirementType.NPC:
                            EditorGUILayout.PropertyField(npcReferenceProperty, new GUIContent($"Reference {i}"));
                            break;
                        case RequirementType.Level:
                            EditorGUILayout.PropertyField(levelReferenceProperty, new GUIContent($"Reference {i}"));
                            break;
                        default:
                            break;
                    } 
                }
                
            }
        }
        questObject.questState = (QuestObject.QuestState)EditorGUILayout.EnumPopup("Quest State", questObject.questState);

        serializedObject.ApplyModifiedProperties(); // Применяем изменения
    }
}*/

