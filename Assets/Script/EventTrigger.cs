using UnityEngine;

public enum EventType
{
    Intro,
    Check1,
    Check2,
    Check3,
    Check4,
    Check5,
    Check6,
    Check7,
    Check8,
    Check9,
    Talk1,
    Talk2,
    Talk3,
    Talk4,
    Talk5,
    Talk6,
    Talk7,
    Talk8,
    Game1start,
    Game1over,
    Opengame1,
    Opengame2,
    Game2over,
    End1,
    End2



}
public class EventTrigger : MonoBehaviour
{
    public EventType eventType;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerDialogEventScript playerDialog = collision.GetComponent<PlayerDialogEventScript>();
            if (playerDialog != null)
            {
                if (CanTriggerEvent())
                {
                    playerDialog.TriggerEventDialog(eventType);
                }
                else
                {
                    Debug.Log($"无法触发事件 {eventType}: 条件未满足");
                }
                
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        PlayerDialogEventScript playerDialog = collision.GetComponent<PlayerDialogEventScript>();
        if (playerDialog != null)
        {
            playerDialog.HideSpaceButton();
        }
    }

    private bool CanTriggerEvent()
    {
        // 检查事件条件
        if (!EventConditions.Instance.AreConditionsMet(eventType))
        {
            return false;
        }

        return true;
    }

}
