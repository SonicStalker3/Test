using System;
using System.Reflection;
using UnityEditor;
using UnityEditorInternal;
using UnityEngine;

namespace Editor.Quests
{
    [CustomEditor(typeof(QuestObject))]
    public class QuestObjectEditor : UnityEditor.Editor
    {
        // This will be the serialized "copy" of YourOtherClass.requirementsProperty
        private SerializedProperty _requirementsProperty;


        private ReorderableList ReferenceList;

        private void OnEnable()
        {
            // Step 1 "link" the SerializedProperties to the properties of YourOtherClass
            _requirementsProperty = serializedObject.FindProperty("requirements");

            // Step 2 setup the ReorderableList
            ReferenceList = new ReorderableList(serializedObject, _requirementsProperty)
            {
                draggable = true,
                displayAdd = true,
                displayRemove = true,
            
                drawHeaderCallback = rect =>
                {
                    EditorGUI.LabelField(rect, "References List");
                },
            
                drawElementCallback = (rect, index, active, focused) =>
                {
                    var element = _requirementsProperty.GetArrayElementAtIndex(index);


                    var typeProperty = element.FindPropertyRelative("type");
                    var descriptionProperty = element.FindPropertyRelative("description");

                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), typeProperty);
                    rect.y += EditorGUIUtility.singleLineHeight;
                    EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), descriptionProperty);
                    rect.y += EditorGUIUtility.singleLineHeight;


                    // В зависимости от типа требования, отображаем соответствующее поле
                    /*switch ((RequirementType)typeProperty.intValue)
                {
                    case RequirementType.Item:
                        referenceProperty = element.FindPropertyRelative("itemReference");
                        EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), referenceProperty);
                        rect.y += EditorGUIUtility.singleLineHeight;
                        //EditorGUILayout.PropertyField(requirements, new GUIContent($"Requirement {i + 1} Type"));// = EditorGUILayout.TextField("Item reference", (string)requirement.reference);
                        break;
                    case RequirementType.NPC:
                        referenceProperty = element.FindPropertyRelative("npcReference");
                        EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), referenceProperty);
                        rect.y += EditorGUIUtility.singleLineHeight;
                        break;
                    case RequirementType.Level:
                        referenceProperty = element.FindPropertyRelative("levelReference");
                        EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), referenceProperty);
                        rect.y += EditorGUIUtility.singleLineHeight;
                        break;
                }*/
                    foreach (var type in Enum.GetValues(typeof(RequirementType)))//(RequirementType)
                    {
                        if ((RequirementType)typeProperty.intValue == (RequirementType)type)
                        {
                            string referenceName = $"{typeProperty.enumDisplayNames[typeProperty.intValue].ToLower()}Reference";
                            string referenceValuesName = $"{typeProperty.enumDisplayNames[typeProperty.intValue].ToLower()}Values";
                            var referenceProperty = element.FindPropertyRelative(referenceName);
                            var referenceValues = element.FindPropertyRelative(referenceValuesName);
                            if (referenceProperty != null)
                            {
                                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), referenceProperty);
                                rect.y += EditorGUIUtility.singleLineHeight;
                            }
                            if (referenceValues != null)
                            {
                                EditorGUI.PropertyField(new Rect(rect.x, rect.y, rect.width, EditorGUIUtility.singleLineHeight), referenceValues);
                                rect.y += EditorGUIUtility.singleLineHeight;
                            }
                        }
                    }
                },


                elementHeight = EditorGUIUtility.singleLineHeight * 5,
            
                onAddCallback = list =>
                {
                    var index = list.serializedProperty.arraySize;
                
                    list.serializedProperty.arraySize++;
                    list.index = index;
                    var element = list.serializedProperty.GetArrayElementAtIndex(index);
                
                    var typeProperty = element.FindPropertyRelative("type");
                    var descriptionProperty = element.FindPropertyRelative("description");
                    foreach (var type in Enum.GetValues(typeof(RequirementType))) //(RequirementType)
                    {
                        string referenceValuesName = $"{typeProperty.enumDisplayNames[typeProperty.intValue].ToLower()}Values";
                        var referenceValues = element.FindPropertyRelative(referenceValuesName);
                        if (referenceValues != null)
                        {
                            FieldInfo fieldInfo = serializedObject.targetObject.GetType().GetField(referenceValuesName);
                            fieldInfo = referenceValues.GetType().GetField("referenceValuesName", BindingFlags.Public | BindingFlags.Instance);
                            RangeAttribute rangeAttribute = (RangeAttribute)fieldInfo?.GetCustomAttribute(typeof(RangeAttribute));
                            /*if (rangeAttribute != null)
                            {
                                Debug.Log($"{rangeAttribute.min}, {rangeAttribute.max}");
                                referenceValues.intValue = (int)rangeAttribute.min;
                            }*/
                        }
                    }

                    typeProperty.intValue = (int) RequirementType.Item;
                    descriptionProperty.stringValue = "";
                }
            };
        }

        public override void OnInspectorGUI()
        {
            serializedObject.Update();
        
            QuestObject questObject = (QuestObject)target;
        
            questObject.Header = EditorGUILayout.TextField("Header", questObject.Header);
            questObject.Description = EditorGUILayout.TextField("Description", questObject.Description);
            questObject.preQuestDialog = (DialogObject)EditorGUILayout.ObjectField("Pre-Quest Dialog", questObject.preQuestDialog, typeof(DialogObject), true);
            questObject.postQuestDialog = (DialogObject)EditorGUILayout.ObjectField("Post-Quest Dialog", questObject.postQuestDialog, typeof(DialogObject), true);
            ReferenceList.DoLayoutList();
            questObject.questState = (QuestObject.QuestState)EditorGUILayout.EnumPopup("Quest State", questObject.questState);
            serializedObject.ApplyModifiedProperties();
        }
    }
}

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

