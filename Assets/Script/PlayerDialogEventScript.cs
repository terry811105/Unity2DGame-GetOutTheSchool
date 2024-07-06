using UnityEngine;
using TMPro;
using System.Collections.Generic;
using System.Linq;


[System.Serializable]
public class Dialog
{
    public string id;
    public string eventType;
    public string character;
    public string text;
    public string emotion;
}

public class PlayerDialogEventScript : MonoBehaviour
{
    public GameObject spaceBtnUI;
    public GameObject dialogUI;
    public TextMeshProUGUI textUI;
    private bool isDialogActive = false;
    private bool canStartDialog = false;
    private List<Dialog> allDialogs;
    private Queue<Dialog> currentDialogQueue;
    private EventType currentEventType;
    private TypewriterEffect typewriterEffect;
    void Start()
    {
        spaceBtnUI.SetActive(false);
        dialogUI.SetActive(false);
        LoadDialogsFromCSV();
        SetupTypewriterEffect();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canStartDialog && !isDialogActive) 
            {
                StartEventDialog(); 
            }
            else if (isDialogActive)
            {
                ShowNextLine();
            }
        }
    }

    public void OnIntroAnimationComplete()
    {
        // 當前導動畫完成時，觸發介紹對話
        // TriggerEventDialog(EventType.Intro);
        currentEventType = EventType.Intro;
        StartEventDialog();

    }

    private void LoadDialogsFromCSV()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("DialogFiled");
        if (csvFile == null)
        {
            Debug.LogError("无法加载对话CSV文件。请确保 'DialogFiled.csv' 文件存在于 Resources 文件夹中。");
            allDialogs = new List<Dialog>(); // 初始化为空列表而不是 null
            return;
        }

        string[] lines = csvFile.text.Split('\n');
        allDialogs = new List<Dialog>();

        // Skip header
        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');
            if (values.Length >= 5)
            {
                Dialog dialog = new Dialog
                {
                    id = values[0],
                    eventType = values[1],
                    character = values[2],
                    text = values[3],
                    emotion = values[4]
                };
                allDialogs.Add(dialog);
            }
        }

        if (allDialogs.Count == 0)
        {
            Debug.LogWarning("加载的对话列表为空。请检查 CSV 文件格式是否正确。");
        }
    }

    public void TriggerEventDialog(EventType eventType)
    {
        List<Dialog> eventDialogs = allDialogs.Where(d => d.eventType == eventType.ToString()).ToList();
        if (eventDialogs.Count > 0)
        {
            currentEventType = eventType;
            ShowSpaceButton();
        }
        else
        {
            Debug.LogWarning($"No dialog found for event type: {eventType}");
        }
    }

    private void ShowSpaceButton()
    {
        spaceBtnUI.SetActive(true);
        canStartDialog = true;
    }

    // 新增方法，用於從外部隱藏提示按鈕（如離開事件點時調用）
    public void HideSpaceButton()
    {
        spaceBtnUI.SetActive(false);
        canStartDialog = false;
    }

     private void StartEventDialog()
    {
        List<Dialog> eventDialogs = allDialogs.Where(d => d.eventType == currentEventType.ToString()).ToList();
        if (eventDialogs.Count > 0)
        {
            StartDialog(eventDialogs);
        }
    }

    private void StartDialog(List<Dialog> dialogs)
    {
        isDialogActive = true;
        canStartDialog = false; // 新增
        spaceBtnUI.SetActive(false); // 新增
        dialogUI.SetActive(true);
        currentDialogQueue = new Queue<Dialog>(dialogs);
        ShowNextLine();
        GetComponent<PlayerMoveScript>().canMove = false;
    }

    private void ShowNextLine()
    {
        if (currentDialogQueue.Count > 0)
        {
            Dialog currentDialog = currentDialogQueue.Dequeue();
            string displayText = $"{currentDialog.character}: {currentDialog.text}";
            typewriterEffect.StartTyping(displayText);
            
            // 這裡可以根據 currentDialog.emotion 設置角色表情或其他視覺效果
        }
        else
        {
            EndDialog();
        }
    }

    private void EndDialog()
    {
        isDialogActive = false;
        dialogUI.SetActive(false);
        GetComponent<PlayerMoveScript>().canMove = true;
    }

    private void SetupTypewriterEffect()
    {
        typewriterEffect = textUI.GetComponent<TypewriterEffect>();
        if (typewriterEffect == null)
        {
            typewriterEffect = textUI.gameObject.AddComponent<TypewriterEffect>();
        }
    }
}
