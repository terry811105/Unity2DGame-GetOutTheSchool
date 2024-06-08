using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogControl : MonoBehaviour
{
    public TextAsset textFiled;
    public TextMeshProUGUI textUI;
    public GameObject promptUI; // ��ܴ��ܪ�UI����
    public GameObject dialogUI; // ��ܮ�UI����

    private bool playerInRange = false;
    private bool dialogUIDidShow = false;

    int displayIndex = 0;
    bool isTextFinish = false;

    string[] lineData;
    string lastText = "";
    void Start()
    {
        promptUI.gameObject.SetActive(false);
        dialogUI.gameObject.SetActive(false);
        GetTextFormFile(textFiled);
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
                Debug.Log("���U�ťի� ��ܹ�ܮ�");
            }

            else if (dialogUIDidShow && isTextFinish == false)
            {
                showText();
            }

            else if (dialogUIDidShow == true && isTextFinish == true)
            {
                dialogUI.SetActive(false);
                dialogUIDidShow = false;
            }

        }

        if (playerInRange == false)
        {
            promptUI.SetActive(false); // ���ô���UI
            dialogUI.SetActive(false);
        }

        
    }

    void GetTextFormFile(TextAsset file)
    {
        //�N�ɮפ�r���Ψ��x�s���r��}�C
        lineData = file.text.Split('\n'); //����N����
        if (lineData.Length > 0 )
        {
            textUI.text = lineData[0];
            displayIndex++;
        }
    }

    void showText()
    {
        
        if (Input.GetKeyDown(KeyCode.Space) && displayIndex < lineData.Length && dialogUIDidShow)
        {
          
            textUI.text = lineData[displayIndex];
            lastText = lineData[displayIndex];
            displayIndex++;
            
        }

        if (displayIndex == lineData.Length)
        {
            isTextFinish = true;
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
            dialogUIDidShow = false;
            Debug.Log("222222");
           
        }
    }
}
