using UnityEngine;
using UnityEngine.UI;

public class ShowClickButton : MonoBehaviour
{
  
    public Button btn;

    public GameObject promptUI; // ��ܴ��ܪ�UI����
    public GameObject dialogUI; // ��ܮ�UI����

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
                // �b���a�a��B���U�ť����Ĳ�o��ܮ�
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
            promptUI.SetActive(true); // ��ܴ���UI
            Debug.Log("1111111");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInRange = false;
            promptUI.SetActive(false); // ���ô���UI
            Debug.Log("222222");
        }
    }
}
