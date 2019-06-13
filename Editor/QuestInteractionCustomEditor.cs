using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEditor;

using Questify.Quests;
using Questify.Runtime;

[CustomEditor(typeof(QuestGiver))]
public class QuestInteractionCustomEditor : Editor
{
    SerializedProperty allAvailableQuests;
    SerializedProperty allOwnedQuests;

    QuestGiver questInteractionTarget;

    private int ownedQuestIndex = 0;
    private int allQuestsIndex = 0;

    private void OnEnable()
    {
        questInteractionTarget = (QuestGiver)target;
        allAvailableQuests = serializedObject.FindProperty("availableQuests");
        allOwnedQuests = serializedObject.FindProperty("ownedQuests");
    }

    public override void OnInspectorGUI()
    {
        // Begin modification check
        EditorGUI.BeginChangeCheck();
        serializedObject.Update();

        ownedQuestIndex = EditorGUILayout.Popup("Select Owned Quest: ", ownedQuestIndex, questInteractionTarget.ownedQuests.Select(x => x.Name).ToArray());
        if (questInteractionTarget.questManager != null)
        {
            allQuestsIndex = EditorGUILayout.Popup("Give Ownership of: ", allQuestsIndex, questInteractionTarget.questManager.Quests.Select(x => x.Name).ToArray());
            EditorGUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if(GUILayout.Button("Give Ownership", GUILayout.MinWidth(150), GUILayout.MaxWidth(170))) { questInteractionTarget.GiveQuestOwnership(questInteractionTarget.questManager.Quests[allQuestsIndex]); }
            if(GUILayout.Button("Revoke Ownership", GUILayout.MinWidth(150), GUILayout.MaxWidth(170))) { allOwnedQuests.DeleteArrayElementAtIndex(ownedQuestIndex); }
            EditorGUILayout.EndHorizontal();
        }
        EditorGUI.EndChangeCheck();

        if (GUI.changed)
        {
            Undo.RecordObject(serializedObject.targetObject, "save");
            serializedObject.ApplyModifiedProperties();
            EditorUtility.SetDirty(serializedObject.targetObject);
            AssetDatabase.SaveAssets();
        }
    }
}
