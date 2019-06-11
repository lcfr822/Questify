using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Questify.Runtime
{
    public class QuestLogItemController : MonoBehaviour
    {
        [SerializeField]
        private Text questText;
        [SerializeField]
        private Image questImage;

        // Start is called before the first frame update
        void Start()
        {
            questImage = GetComponent<Image>();
            questText = GetComponentInChildren<Text>();
        }

        public void ManualInitialization()
        {
            questImage = GetComponent<Image>();
            questText = GetComponentInChildren<Text>();
        }

        public void AssignQuestLogText(string newQuestText) { questText.text = newQuestText; }

        public void AssignQuestLogImage(Sprite questLogSprite) { questImage.sprite = questLogSprite; }
    }
}