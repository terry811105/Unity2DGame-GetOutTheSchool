using UnityEngine;

public enum EventType
{
    Intro,
    Check1,
    Check2,
    Check3,
    Talk1,
    Game1,
    DirtyTable,
    Game2


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
                playerDialog.TriggerEventDialog(eventType);
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

}
