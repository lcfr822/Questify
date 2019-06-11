using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.UI;

using Questify.Quests;
using Questify.Management;

namespace Questify.Runtime
{
    public class QuestLog : MonoBehaviour
    {
        private CanvasGroup logGroup;

        [SerializeField]
        private Transform questLogSpawner;
        [SerializeField]
        private GameObject questLogObjectPrefab;
        [SerializeField]
        private RectTransform content;
        [SerializeField]
        private int numberOfLogs = 0;

        private int tempQuestIndex = 0; // Allows cycling of test quests.

        public List<Quest> activeQuests = new List<Quest>();
        public List<GameObject> activeQuestObjects = new List<GameObject>();
        public QuestManager questManager;

        // Start is called before the first frame update
        void Start()
        {
            logGroup = GetComponent<CanvasGroup>();
            content.sizeDelta = new Vector2(0, numberOfLogs * 200);
            ShowHideLogGroup();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.L)) { ShowHideLogGroup(); }

            // DEBUG COMMANDS
            if (Input.GetKeyDown(KeyCode.N) && tempQuestIndex < questManager.Quests.Count) { InitiateQuest(questManager.Quests[tempQuestIndex]); tempQuestIndex++; }
            else if (Input.GetKeyDown(KeyCode.P) && tempQuestIndex > 0) { Debug.Log(tempQuestIndex - 1 + ", " + (questManager.Quests.Count)); RemoveQuestFromLog(questManager.Quests[tempQuestIndex - 1]); tempQuestIndex--; }
        }

        public void InitiateQuest(Quest quest)
        {
            quest.ActiveQuestStage = quest.QuestStages[0];
            activeQuests.Add(quest);
            AddQuestToLog(quest);
        }

        private void AddQuestToLog(Quest newQuest)
        {
            float positionY = numberOfLogs * 200;
            Vector3 spawnPosition = new Vector3(questLogSpawner.position.x, -positionY, questLogSpawner.rotation.z);
            GameObject spawnedLog = Instantiate(questLogObjectPrefab, spawnPosition, questLogSpawner.rotation);
            spawnedLog.GetComponent<QuestLogItemController>().ManualInitialization();
            spawnedLog.transform.SetParent(questLogSpawner, false);
            spawnedLog.GetComponent<QuestLogItemController>().AssignQuestLogText(newQuest.Name + "\n" + newQuest.ActiveQuestStage.stageName + "\n" + newQuest.ActiveQuestStage.stageDescription);
            activeQuestObjects.Add(spawnedLog);
            numberOfLogs++;
        }

        private void RemoveQuestFromLog(Quest oldQuest)
        {
            Destroy(activeQuestObjects[activeQuests.IndexOf(oldQuest)].gameObject);
            activeQuestObjects.Remove(activeQuestObjects[activeQuests.IndexOf(oldQuest)]);
            activeQuests.Remove(oldQuest);
            numberOfLogs--;
        }

        private void ShowHideLogGroup()
        {
            if (logGroup.alpha > 0)
            {
                logGroup.alpha = 0;
                logGroup.interactable = false;
                logGroup.blocksRaycasts = false;
            }
            else
            {
                logGroup.alpha = 1;
                logGroup.interactable = true;
                logGroup.blocksRaycasts = true;
            }
        }
    }
}