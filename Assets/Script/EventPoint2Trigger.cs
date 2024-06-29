using TMPro;
using UnityEngine;

public class EventPoint2Trigger : MonoBehaviour
{
    public GameObject promptUI; 
    public GameObject dialogUI; // 對話框UI物件
    public TextMeshProUGUI textUI;
    public TextAsset txtFiled;
    
    bool isPlayerInRange = false;
    bool isDialogShown = false;

    int displayIndex = 0;
    bool isTextFinish = false;

    string[] lineData;
    
    private TypewriterEffect typewriterEffect;

    void Start()
    {
        promptUI.SetActive(false);
        dialogUI.SetActive(false);

        typewriterEffect = textUI.GetComponent<TypewriterEffect>();
         if (typewriterEffect == null)
        {
            typewriterEffect = textUI.gameObject.AddComponent<TypewriterEffect>();
        }

        lineData = TxtFiledManager.GetTextFromTxt(txtFiled);
    }

    // Update is called once per frame
    void Update()
    {
        showDialogUI();
    }

    void showDialogUI()
    {
        if (isPlayerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            if (!isDialogShown)
            {
                // 顯示對話UI並開始第一行文字
                dialogUI.SetActive(true);
                isDialogShown = true;
                isTextFinish = false;
                displayIndex = 0;
                nextText();
            }
            else if (!isTextFinish)
            {
                // 跳到下一句
                nextText();
            }
            else
            {
                // 關閉對話UI
                dialogUI.SetActive(false);
                isDialogShown = false;
            }
        }
        
    }

    void nextText() 
    {
        if (displayIndex < lineData.Length)
        {
            typewriterEffect.StartTyping(lineData[displayIndex]);
            displayIndex++;
            if (displayIndex == lineData.Length)
            {
                isTextFinish = true;
                StaticData.checkWindow = true;
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
           promptUI.SetActive(true);
           isPlayerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            promptUI.SetActive(false);
            isPlayerInRange = false;
        }
    }
}
