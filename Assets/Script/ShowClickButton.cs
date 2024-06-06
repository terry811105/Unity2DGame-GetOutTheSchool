using UnityEngine;
using UnityEngine.UI;

public class ShowClickButton : MonoBehaviour
{
  
    public Button btn;

    public GameObject promptUI; // 顯示提示的UI物件
    public GameObject dialogUI; // 對話框UI物件

    private bool playerInRange = false;
    private bool dialogUIDidShow = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        promptUI.gameObject.SetActive(false);
        dialogUI.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogUIDidShow == false)
            {
                // 在玩家靠近且按下空白鍵時觸發對話框
                dialogUI.SetActive(true);
                dialogUIDidShow = true;
            } else
            {
                dialogUI.SetActive(false);
                dialogUIDidShow = false;
            }
            
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player")) 
        {
            playerInRange = true;
            promptUI.SetActive(true); // 顯示提示UI
            Debug.Log("1111111");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            promptUI.SetActive(false); // 隱藏提示UI
            Debug.Log("222222");
        }
    }
}
