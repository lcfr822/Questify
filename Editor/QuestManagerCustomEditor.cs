using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;
using UnityEditor;

using Questify.Management;
using Questify.Quests;

[CustomEditor(typeof(QuestManager))]
public class QuestManagerCustomEditor : Editor
{
    QuestManager m_QManager;

    void OnEnable()
    {
        m_QManager = (QuestManager)target;
    }

    public override void OnInspectorGUI()
    {
        if(GUILayout.Button("Open Editor")) { QuestManagerEditorWindow.ShowQuestEditorWindow(m_QManager); }
    }
}