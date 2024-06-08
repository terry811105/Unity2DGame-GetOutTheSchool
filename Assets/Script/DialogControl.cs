using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DialogControl : MonoBehaviour
{
    public TextAsset textFiled;
    public TextMeshProUGUI textUI;
    public GameObject promptUI; // 顯示提示的UI物件
    public GameObject dialogUI; // 對話框UI物件

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
                // 在玩家靠近且按下空白鍵時觸發對話框
                dialogUI.SetActive(true);
                dialogUIDidShow = true;
                Debug.Log("按下空白建 顯示對話框");
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
            promptUI.SetActive(false); // 隱藏提示UI
            dialogUI.SetActive(false);
        }

        
    }

    void GetTextFormFile(TextAsset file)
    {
        //將檔案文字分割並儲存成字串陣列
        lineData = file.text.Split('\n'); //換行就切割
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
            promptUI.SetActive(true); // 顯示提示UI
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
