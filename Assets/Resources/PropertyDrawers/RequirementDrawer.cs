/*
using UnityEngine;
using UnityEditor;

public class RequirementDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        base.OnGUI(position, property, label);
        // EditorGUI.BeginProperty(position, label, property);

        // Get the serialized properties for type and description
        SerializedProperty typeProp = property.FindPropertyRelative("type");
        SerializedProperty descriptionProp = property.FindPropertyRelative("description");

        // Calculate the position for the type field
        Rect typeRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(typeRect, typeProp);

        // Calculate the position for the description field
        Rect descRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing,
                                 position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(descRect, descriptionProp);

        // Show the appropriate reference field based on the selected type
        switch ((RequirementType)typeProp.enumValueIndex)
        {
            case RequirementType.Item:
                SerializedProperty itemRefProp = property.FindPropertyRelative("itemReference");
                Rect itemRect = new Rect(position.x, descRect.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing,
                                         position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(itemRect, itemRefProp);
                break;
            case RequirementType.NPC:
                SerializedProperty npcRefProp = property.FindPropertyRelative("npcReference");
                Rect npcRect = new Rect(position.x, descRect.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing,
                                        position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(npcRect, npcRefProp);
                break;
            case RequirementType.Level:
                SerializedProperty levelRefProp = property.FindPropertyRelative("levelReference");
                Rect levelRect = new Rect(position.x, descRect.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing,
                                          position.width, EditorGUIUtility.singleLineHeight);
                EditorGUI.PropertyField(levelRect, levelRefProp);
                break;
        }

        EditorGUI.EndProperty();
    }
}

// Custom attribute to display the RequirementType enum as a dropdown
public class RequirementTypeAttribute : PropertyAttribute { }

[CustomPropertyDrawer(typeof(Requirement))]
public class RequirementPropertyDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, property);

        // Get the serialized properties for type and description
        SerializedProperty typeProp = property.FindPropertyRelative("type");
        SerializedProperty descriptionProp = property.FindPropertyRelative("description");

        // Calculate the position for the type field
        Rect typeRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(typeRect, typeProp);

        // Calculate the position for the description field
        Rect descRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + EditorGUIUtility.standardVerticalSpacing,
                                 position.width, EditorGUIUtility.singleLineHeight);
        EditorGUI.PropertyField(descRect, descriptionProp);

        EditorGUI.EndProperty();
    }
}
*/
