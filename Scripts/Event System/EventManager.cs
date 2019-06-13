using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using Questify.Quests;

[System.Serializable]
public class GameObjectEvent : UnityEvent<GameObject> { }

[System.Serializable]
public class QuestEvent : UnityEvent<Quest> { }

[System.Serializable]
public class QuestStageEvent : UnityEvent<QuestStage> { }

public class EventManager : MonoBehaviour
{
    private Dictionary<string, UnityEvent<GameObject>> eventDictionary;
    private Dictionary<string, UnityEvent<Quest>> questEventDictionary;
    private Dictionary<string, UnityEvent<QuestStage>> questStageEventDictionary;

    private static EventManager eventManager;

    /// <summary>
    /// Singleton pattern implementation, prevents multiple triggers and allows static reference to non-static class.
    /// </summary>
    public static EventManager instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;
                if (!eventManager)
                {
                    Debug.LogError("There needs to be one active EventManager script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    /// <summary>
    /// Initialize the event dictionary to an empty dictionary.
    /// </summary>
    void Init()
    {
        if (eventDictionary == null) { eventDictionary = new Dictionary<string, UnityEvent<GameObject>>(); }
        if (questEventDictionary == null) { questEventDictionary = new Dictionary<string, UnityEvent<Quest>>(); }
        if (questStageEventDictionary == null) { questStageEventDictionary = new Dictionary<string, UnityEvent<QuestStage>>(); }
    }

    /// <summary>
    /// Subscribe a class to an Event of name contained in eventName.
    /// </summary>
    /// <param name="eventName">Name of the event to listen for.</param>
    /// <param name="listener">Action variable from the subscribing class (must not be null).</param>
    public static void StartListening(string eventName, UnityAction<GameObject> listener)
    {
        UnityEvent<GameObject> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new GameObjectEvent();
            thisEvent.AddListener(listener);
            instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    /// <summary>
    /// Subscribe a class to an Event of name contained in eventName.
    /// </summary>
    /// <param name="eventName">Name of the event to listen for.</param>
    /// <param name="listener">Action variable from the subscribing class (must not be null).</param>
    public static void StartListening(string eventName, UnityAction<Quest> listener)
    {
        UnityEvent<Quest> thisEvent = null;
        if (instance.questEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new QuestEvent();
            thisEvent.AddListener(listener);
            instance.questEventDictionary.Add(eventName, thisEvent);
        }
    }

    /// <summary>
    /// Subscribe a class to an Event of name contained in eventName.
    /// </summary>
    /// <param name="eventName">Name of the event to listen for.</param>
    /// <param name="listener">Action variable from the subscribing class (must not be null).</param>
    public static void StartListening(string eventName, UnityAction<QuestStage> listener)
    {
        UnityEvent<QuestStage> thisEvent = null;
        if (instance.questStageEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.AddListener(listener);
        }
        else
        {
            thisEvent = new QuestStageEvent();
            thisEvent.AddListener(listener);
            instance.questStageEventDictionary.Add(eventName, thisEvent);
        }
    }

    /// <summary>
    /// Unsubscribe a specific listener from an Event.
    /// </summary>
    /// <param name="eventName">Name of the event to stop listening for.</param>
    /// <param name="listener">Action variable from the subscribing class (must not be null).</param>
    public static void StopListening(string eventName, UnityAction<GameObject> listener)
    {
        if (eventManager == null) { return; }
        UnityEvent<GameObject> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    /// <summary>
    /// Unsubscribe a specific listener from an Event.
    /// </summary>
    /// <param name="eventName">Name of the event to stop listening for.</param>
    /// <param name="listener">Action variable from the subscribing class (must not be null).</param>
    public static void StopListening(string eventName, UnityAction<Quest> listener)
    {
        if (eventManager == null) { return; }
        UnityEvent<Quest> thisEvent = null;
        if (instance.questEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    /// <summary>
    /// Unsubscribe a specific listener from an Event.
    /// </summary>
    /// <param name="eventName">Name of the event to stop listening for.</param>
    /// <param name="listener">Action variable from the subscribing class (must not be null).</param>
    public static void StopListening(string eventName, UnityAction<QuestStage> listener)
    {
        if (eventManager == null) { return; }
        UnityEvent<QuestStage> thisEvent = null;
        if (instance.questStageEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.RemoveListener(listener);
        }
    }

    /// <summary>
    /// Execute an event by name.
    /// </summary>
    /// <param name="eventName">Name of the event to trigger.</param>
    /// <param name="eventObject">GameObject to be passed to listeners' handler function (must not be null).</param>
    public static void TriggerEvent(string eventName, GameObject eventObject)
    {
        UnityEvent<GameObject> thisEvent = null;
        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(eventObject);
        }
    }

    /// <summary>
    /// Execute an event by name.
    /// </summary>
    /// <param name="eventName">Name of the event to trigger.</param>
    /// <param name="eventObject">Quest to be passed to listeners' handler function (must not be null).</param>
    public static void TriggerEvent(string eventName, Quest eventObject)
    {
        UnityEvent<Quest> thisEvent = null;
        if (instance.questEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(eventObject);
        }
    }

    /// <summary>
    /// Execute an event by name.
    /// </summary>
    /// <param name="eventName">Name of the event to trigger.</param>
    /// <param name="eventObject">QuestStage to be passed to listeners' handler function (must not be null).</param>
    public static void TriggerEvent(string eventName, QuestStage eventObject)
    {
        UnityEvent<QuestStage> thisEvent = null;
        if (instance.questStageEventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(eventObject);
        }
    }
}
