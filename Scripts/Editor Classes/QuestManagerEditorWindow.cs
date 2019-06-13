using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEditor;
using UnityEditor.SceneManagement;

using Questify.Management;
using Questify.Quests;

public class QuestManagerEditorWindow : EditorWindow
{
    QuestManager m_QManager;
    SerializedObject s_QManager;
    SerializedProperty m_Quests;
    SerializedProperty m_Quest;
    SerializedProperty m_QuestStage;
    SerializedProperty m_QuestStages;

    bool autoSave = true;
    int questIndex = 0;
    int questStageIndex = 0;

    static object targetObject;

    [MenuItem("Window/Quest Management")]
    public static void ShowWindow()
    {
        GetWindow(typeof(QuestManagerEditorWindow));
    }

    public static void ShowQuestEditorWindow(object target)
    {
        var window = GetWindow<QuestManagerEditorWindow>();
        window.m_QManager = target as QuestManager;
        window.s_QManager = new SerializedObject(window.m_QManager);
        window.m_Quests = window.s_QManager.FindProperty("Quests");
        window.titleContent = new GUIContent("Quest Editor");
    }

    private void OnGUI()
    {
        // Save Control Group
        GUILayout.BeginHorizontal();
        autoSave = GUILayout.Toggle(autoSave, "Auto-Save");
        if (!autoSave && GUILayout.Button("Save")) { ManualSave(); }
        GUILayout.EndHorizontal();

        // Quest Creation, Deletion, Editing, and Selection Crontrol Group
        GUILayout.Label("Editing: " + s_QManager.targetObject.name, EditorStyles.boldLabel);
        questIndex = EditorGUILayout.Popup("Select Quest: ", questIndex, m_QManager.Quests.Select(x => x.Name).ToArray());
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Add Quest", GUILayout.ExpandWidth(true), GUILayout.MinWidth(100), GUILayout.MaxWidth(150))) { m_QManager.AddQuest(); }
        if (GUILayout.Button("Delete Quest", GUILayout.ExpandWidth(true), GUILayout.MinWidth(100), GUILayout.MaxWidth(150)))
        {
            if (questIndex > 0)
            {
                m_Quests.DeleteArrayElementAtIndex(questIndex);
                questIndex--;
                m_Quest = m_Quest.GetArrayElementAtIndex(questIndex);
            }
            else { return; }
        }
        EditorGUILayout.EndHorizontal();

        // Isolate selected Quest
        m_Quest = m_Quests.GetArrayElementAtIndex(questIndex);
        SerializedProperty questName = m_Quest.FindPropertyRelative("Name");
        SerializedProperty questDescription = m_Quest.FindPropertyRelative("Description");
        SerializedProperty questRewardType = m_Quest.FindPropertyRelative("RewardTypes");
        SerializedProperty questReward = m_Quest.FindPropertyRelative("QuestReward");

        // Begin Modification Control
        EditorGUI.BeginChangeCheck();
        s_QManager.Update();

        // Quest Stage Creation, Deletion, Editing, and Selection Control Group
        questStageIndex = EditorGUILayout.Popup("Select Quest Stage: ", questStageIndex, m_QManager.Quests[questIndex].QuestStages.Select(x => x.stageName).ToArray());
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button("Add Stage", GUILayout.ExpandWidth(true), GUILayout.MinWidth(100), GUILayout.MaxWidth(150))) { m_QManager.AddStage(m_QManager.Quests[questIndex]); }
        if (GUILayout.Button("Delete Stage", GUILayout.ExpandWidth(true), GUILayout.MinWidth(100), GUILayout.MaxWidth(150))) { m_QManager.DeleteStage(m_QManager.Quests[questIndex], m_QManager.Quests[questIndex].QuestStages[questStageIndex]); }
        EditorGUILayout.EndHorizontal();

        // Isolate selected Quest Stage
        IEnumerator questStageEnum = m_Quest.GetEnumerator();
        while (questStageEnum.MoveNext())
        {
            SerializedProperty current = questStageEnum.Current as SerializedProperty;
            if (current.name == "QuestStages") { m_QuestStages = current; break; }
        }
        m_QuestStage = m_QuestStages.GetArrayElementAtIndex(questStageIndex);
        SerializedProperty questStageName = m_QuestStage.FindPropertyRelative("stageName");
        SerializedProperty questStageDescription = m_QuestStage.FindPropertyRelative("stageDescription");
        SerializedProperty questStageHidden = m_QuestStage.FindPropertyRelative("stageHidden");
        SerializedProperty questStageOptional = m_QuestStage.FindPropertyRelative("stageOptional");

        InsertGUISpaces(2);
        EditorGUILayout.LabelField("Quest[" + questIndex + "]: " + m_QManager.Quests[questIndex].Name);
        EditorGUILayout.PropertyField(questName);
        EditorGUILayout.PropertyField(questDescription);
        EditorGUILayout.PropertyField(questReward);
        InsertGUISpaces(2);
        EditorGUILayout.LabelField("Stage[" + questStageIndex + "]: " + m_QManager.Quests[questIndex].QuestStages[questStageIndex].stageName);
        EditorGUILayout.PropertyField(questStageName);
        EditorGUILayout.PropertyField(questStageDescription);
        EditorGUILayout.PropertyField(questStageHidden);
        EditorGUILayout.PropertyField(questStageOptional);

        EditorGUI.EndChangeCheck();
        // End Modification Control

        // Automatic Saving
        if (GUI.changed && autoSave)
        {
            Undo.RecordObject(s_QManager.targetObject, "save");
            s_QManager.ApplyModifiedProperties();
            EditorUtility.SetDirty(s_QManager.targetObject);
            AssetDatabase.SaveAssets();
            Debug.LogWarning("Auto-Saved: " + s_QManager.targetObject.name);
        }
    }

    /// <summary>
    /// Insert N vertical spaces into the the Editor GUI.
    /// </summary>
    /// <param name="count">int: count of spaces to insert.</param>
    private void InsertGUISpaces(int count)
    {
        for (int i = 0; i < count; i++) { EditorGUILayout.Space(); }
    }

    /// <summary>
    /// Manually save changes made to selected Quest Manager.
    /// </summary>
    private void ManualSave()
    {
        Undo.RecordObject(s_QManager.targetObject, "save");
        s_QManager.ApplyModifiedProperties();
        EditorUtility.SetDirty(s_QManager.targetObject);
        AssetDatabase.SaveAssets();
        Debug.LogWarning("Manually-Saved: " + s_QManager.targetObject.name);
    }
}
