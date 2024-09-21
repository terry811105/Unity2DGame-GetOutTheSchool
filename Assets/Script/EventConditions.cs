using UnityEngine;
using System.Collections.Generic;

public class EventConditions : MonoBehaviour
{
    public static EventConditions Instance { get; private set; }

    private Dictionary<EventType, List<EventType>> eventRequirements = new Dictionary<EventType, List<EventType>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeEventRequirements();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeEventRequirements()
    {
        // 在这里定义每个事件的条件
        // 例如：Talk3 需要完成 Check1
        // AddEventRequirement(EventType.Talk3, EventType.Talk1);

        // 添加更多事件条件...
        // 例如：
        // AddEventRequirement(EventType.Talk4, EventType.Check2);
        // AddEventRequirement(EventType.Talk5, EventType.Check3);
        // AddEventRequirement(EventType.Game1start, EventType.Talk1);
        // AddEventRequirement(EventType.Game1start, EventType.Talk2);
    }

    // 加入事件條件
    public void AddEventRequirement(EventType eventType, EventType requiredEvent)
    {
        if (!eventRequirements.ContainsKey(eventType))
        {
            eventRequirements[eventType] = new List<EventType>();
        }
        Debug.Log($"{eventType}:加入事件條件");
        eventRequirements[eventType].Add(requiredEvent);
    }

    public bool AreConditionsMet(EventType eventType)
    {
        if (!eventRequirements.ContainsKey(eventType))
        {
            Debug.Log($"{eventType}:没有定义条件 默认可以触发");
            return true; // 如果没有定义条件，默认可以触发
        }

        foreach (EventType requiredEvent in eventRequirements[eventType])
        {
            if (!PlayerProgress.Instance.IsEventCompleted(requiredEvent.ToString()))
            {
                Debug.Log($"{eventType}:條件未滿足");
                return false;
            }
        }

        return true;
    }


}
