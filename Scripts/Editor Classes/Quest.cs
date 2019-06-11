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
        public string Name = "New Quest";
        public string Description = "New Quest Description";
        public QuestStage ActiveQuestStage = null;
        public List<QuestStage> QuestStages = new List<QuestStage>();

        public Quest(string questName, string questDescription, List<QuestStage> questStages)
        {
            Name = questName;
            Description = questDescription;
            QuestStages = questStages;
            if(QuestStages[0] != null) { ActiveQuestStage = QuestStages[0]; }
        }

        public QuestStage GetStageByName(string name)
        {
            if(QuestStages.Where(x => x.stageName == name) != null) { return QuestStages.Where(x => x.stageName == name) as QuestStage; }
            else { return null; }
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
        public string stageName = "New Quest Stage Name";
        public string stageDescription = "New Quest Stage Description";

        public bool stageHidden = false;
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