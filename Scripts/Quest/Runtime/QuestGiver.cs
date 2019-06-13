using System.Collections;
using System.Collections.Generic;

using UnityEngine;

using Questify.Management;
using Questify.Quests;

namespace Questify.Runtime
{
    public class QuestGiver : MonoBehaviour
    {
        [SerializeField]
        public List<Quest> ownedQuests = new List<Quest>();
        [SerializeField]
        public List<Quest> availableQuests = new List<Quest>();
        public QuestManager questManager;

        // Start is called before the first frame update
        void Start()
        {
            questManager = FindObjectOfType<QuestLog>().questManager;
        }

        // Update is called once per frame
        void Update()
        {

        }

        /// <summary>
        /// Assign a Quest to this Quest Giver.
        /// </summary>
        /// <param name="quest">Quest to be owned.</param>
        public void GiveQuestOwnership(Quest quest)
        {
            ownedQuests.Add(quest);
        }

        /// <summary>
        /// Remove a Quest from this Quest Giver.
        /// </summary>
        /// <param name="quest">Quest to be unowned.</param>
        public void RevokeQuestOwnership(Quest quest)
        {
            ownedQuests.Remove(quest);
        }

        /// <summary>
        /// Retrieve a Quest by index.
        /// </summary>
        /// <param name="index">int: index of a Quest this Quest Giver owns.</param>
        /// <returns>Quest</returns>
        public void AssignOwnedQuest(int index)
        {
            FindObjectOfType<QuestLog>().GiveQuest(ownedQuests[index]);
        }
    }
}