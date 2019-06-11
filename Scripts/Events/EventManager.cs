using System.Collections;
using System.Collections.Generic;

using UnityEngine;
using UnityEngine.Events;

using Questify.Quests;

[System.Serializable]
public class GameObjectEvent : UnityEvent<GameObject> { }

[System.Serializable]
public class QuestEvent : UnityEvent<Quest> { }

public class EventManager : MonoBehaviour
{
    private Dictionary<string, UnityEvent<GameObject>> eventDictionary;
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
                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
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
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityEvent<GameObject>>();
        }
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
}
