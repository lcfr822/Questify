using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

using Questify.Quests;

namespace Questify.Management
{
    [CreateAssetMenu]
    public class QuestManager : ScriptableObject
    {
        [SerializeField]
        public List<Quest> Quests = new List<Quest>(); // All Possible Quests

        public void OnEnable()
        {
            if (Quests.Count == 0)
            {
                Quests.Add(new Quest("Test Quest", "This is an example of how quests are managed.",
                    new List<QuestStage> {
                        new QuestStage("Stage 1", "This is an example of a quest stage.", false), new QuestStage("Stage 2", "This is an example of a hidden quest stage.", true)
                    }));
            }
        }

        public void AddQuest()
        {
            Quests.Add(new Quest("New Quest " + (Quests.Count + 1).ToString(), "Default Quest Description", new List<QuestStage>() { new QuestStage("Stage 1", "Default Stage Description", false) }));
        }

        public void DeleteQuest(Quest quest)
        {
            Quests.Remove(quest);
        }

        public void AddStage(Quest quest)
        {
            quest.QuestStages.Add(new QuestStage("Stage " + (quest.QuestStages.Count + 1).ToString(), "Default QuestStage Description.", false));
        }

        public void DeleteStage(Quest quest, QuestStage stage)
        {
            quest.QuestStages.Remove(stage);
        }

        public Quest GetQuestByName(string name)
        {
            if (Quests.Where(x => x.Name == name) != null) { return Quests.Where(x => x.Name == name) as Quest; }
            else { return null; }
        }
    }
}
