using UnityEngine;
using System.Collections.Generic;

public class PlayerProgress : MonoBehaviour
{
    public static PlayerProgress Instance { get; private set; }

    private HashSet<string> completedEvents = new HashSet<string>();
    private HashSet<string> collectedItems = new HashSet<string>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void CompleteEvent(string eventName)
    {
        completedEvents.Add(eventName);
    }

    public bool IsEventCompleted(string eventName)
    {
        return completedEvents.Contains(eventName);
    }

    public void CollectItem(string itemName)
    {
        collectedItems.Add(itemName);
    }

    public bool HasItem(string itemName)
    {
        return collectedItems.Contains(itemName);
    }
}
