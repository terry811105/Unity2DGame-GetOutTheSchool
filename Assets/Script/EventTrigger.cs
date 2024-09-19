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
