using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Questify.Quests
{
    [System.Serializable]
    //public class Quest : IQuest
    public class Quest
    {
        [Tooltip("Give this quest a name.")]
        public string Name = "New Quest";
        [Tooltip("Briefly describe the story of the quest.")]
        public string Description = "New Quest Description";
        [Tooltip("Reward granted upon Quest completion.")]
        public QuestReward QuestReward = null;
        public int currentStageIndex = 0;
        public QuestStage ActiveQuestStage = null;
        public List<QuestStage> QuestStages = new List<QuestStage>();

        public Quest(string questName, string questDescription, List<QuestStage> questStages)
        {
            Name = questName;
            Description = questDescription;
            QuestStages = questStages;
            if(QuestStages[currentStageIndex] != null) { ActiveQuestStage = QuestStages[currentStageIndex]; }
        }

        /// <summary>
        /// Retrieve a QuestStage using it's name.
        /// </summary>
        /// <param name="name">String representing a QuestStage.</param>
        /// <returns></returns>
        public QuestStage GetStageByName(string name)
        {
            if(QuestStages.Where(x => x.stageName == name) != null) { return QuestStages.Where(x => x.stageName == name) as QuestStage; }
            else { return null; }
        }

        public void SetActiveStage(int stageIndex)
        {
            ActiveQuestStage = QuestStages[stageIndex];
        }

        public void CompleteQuest()
        {
            
        }

        public void CompleteQuestStage(Quest quest, int stageIndex)
        {
            quest.QuestStages[stageIndex].completed = true;
            quest.currentStageIndex++;
        }

        public override string ToString()
        {
            string questString = Name + "\n";
            questString += "Number of Stages: " + QuestStages.Count + "\n";

            return questString;
        }
    }

    [System.Serializable]
    public class QuestStage
    {
        [Tooltip("Give this Quest Stage a name.")]
        public string stageName = "New Quest Stage Name";
        [Tooltip("Briefly describe the action(s) necessary to complete this Quest Stage.")]
        public string stageDescription = "New Quest Stage Description";

        public bool stageHidden = false;
        public bool stageOptional = false;
        public bool completed = false;
        public bool failed = false;

        public QuestStage(string newName, string newDescription, bool hideStage)
        {
            stageName = newName;
            stageDescription = newDescription;
            stageHidden = hideStage;
        }
    }
}