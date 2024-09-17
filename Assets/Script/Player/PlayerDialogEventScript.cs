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
    public bool isChoice;
    public string[] choices;
    public string[] nextDialogIds;
}

public class PlayerDialogEventScript : MonoBehaviour
{
    public GameObject spaceBtnUI;
    public GameObject dialogUI;
    public GameObject ghost;
    public TextMeshProUGUI textUI;
    private bool isDialogActive = false;
    private bool canStartDialog = false;
    private List<Dialog> allDialogs;
    private Queue<Dialog> currentDialogQueue;
    private EventType currentEventType;
    private TypewriterEffect typewriterEffect;
    public TextMeshProUGUI[] choiceTexts; // 新增：选择文本数组
    private int currentChoiceIndex = 0; // 新增：当前选择的索引
    private bool isShowingChoices = false; // 新增：是否正在显示选择
    public GameObject player;
    public GameObject littleGameParent;
    public GameObject keyGamePrefab;
    public GameObject puzzleGamePrefab;
    public AudioSource audioSource;  
    public AudioClip soundEffect;
    void Start()
    {
        spaceBtnUI.SetActive(false);
        dialogUI.SetActive(false);
        ghost.SetActive(false);
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
            else if (isDialogActive && !isShowingChoices)
            {
                ShowNextLine();
            }
        }

        if (isShowingChoices)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                currentChoiceIndex = (currentChoiceIndex - 1 + choiceTexts.Length) % choiceTexts.Length;
                UpdateChoiceHighlight();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                currentChoiceIndex = (currentChoiceIndex + 1) % choiceTexts.Length;
                UpdateChoiceHighlight();
            }
            else if (Input.GetKeyDown(KeyCode.Return))
            {
                MakeChoice();
            }
        }
    }

    public void OnIntroAnimationComplete()
    {
        // 當前導動畫完成時，觸發介紹對話
        // TriggerEventDialog(EventType.Intro);
        currentEventType = EventType.Intro;
        audioSource.PlayOneShot(soundEffect);
        StartEventDialog();

    }

    private void LoadDialogsFromCSV()
    {
        TextAsset csvFile = Resources.Load<TextAsset>("DialogFiled");
        if (csvFile == null)
        {
            Debug.LogError("无法加载对话CSV文件。请确保 'DialogFiled.csv' 文件存在于 Resources 文件夹中。");
            allDialogs = new List<Dialog>();
            return;
        }

        string[] lines = csvFile.text.Split('\n');
        allDialogs = new List<Dialog>();

        // Skip header
        for (int i = 1; i < lines.Length; i++)
        {
            string[] values = lines[i].Split(',');
            if (values.Length >= 9) // 确保有足够的列
            {
                Dialog dialog = new Dialog
                {
                    id = values[0],
                    eventType = values[1],
                    character = values[2],
                    text = values[3],
                    emotion = values[4],
                    // Remark 和 Condition 字段在这里被跳过，因为 Dialog 类中没有相应的属性
                    isChoice = false,
                    choices = values[8].Split(';'),
                    nextDialogIds = values[9].Split(';')
                };
                if (values[7] == "TRUE")
                {
                    dialog.isChoice = true;
                } 
                else
                {
                    dialog.isChoice = false;
                }

                // 处理空字符串的情况
                if (string.IsNullOrEmpty(values[8]))
                {
                    dialog.choices = new string[0];
                }
                if (string.IsNullOrEmpty(values[9]))
                {
                    dialog.nextDialogIds = new string[0];
                }

                Debug.Log("加载的对话: " + dialog.text);
                allDialogs.Add(dialog);
            }
            else
            {
                Debug.LogWarning($"第 {i + 1} 行的数据不完整，已跳过。");
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
        if (spaceBtnUI != null)
        {
            spaceBtnUI.SetActive(false);
        }
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
        GetComponent<PlayerMoveScript>().PlayIdleAnimation();
        if (currentEventType == EventType.Talk1)
        {
            ghost.SetActive(true);
        }
    }

    private void ShowNextLine()
    {
        if (currentDialogQueue.Count > 0)
        {
            Dialog currentDialog = currentDialogQueue.Peek();
            
            if (currentDialog.isChoice)
            {
                ShowChoices(currentDialog);
            }
            else
            {
                isShowingChoices = false;
                string displayText = $"{currentDialog.character}: {currentDialog.text}";
                typewriterEffect.StartTyping(displayText);
                currentDialogQueue.Dequeue();
            }
        }
        else
        {
            EndDialog();
        }
    }

    private void ShowChoices(Dialog dialog)
    {
        isShowingChoices = true;
        currentChoiceIndex = 0;

        textUI.text = dialog.text; // 显示选择提示文本

        for (int i = 0; i < choiceTexts.Length; i++)
        {
            if (i < dialog.choices.Length)
            {
                choiceTexts[i].gameObject.SetActive(true);
                choiceTexts[i].text = dialog.choices[i];
            }
            else
            {
                choiceTexts[i].gameObject.SetActive(false);
            }
        }

        UpdateChoiceHighlight();
    }

    private void UpdateChoiceHighlight()
    {
        for (int i = 0; i < choiceTexts.Length; i++)
        {
            choiceTexts[i].color = (i == currentChoiceIndex) ? Color.yellow : Color.white;
        }
    }

    private void MakeChoice()
    {
        Dialog currentDialog = currentDialogQueue.Dequeue();
        string nextDialogId = currentDialog.nextDialogIds[currentChoiceIndex];
        
        Dialog nextDialog = allDialogs.FirstOrDefault(d => d.id == nextDialogId);
        if (nextDialog != null)
        {
            currentDialogQueue.Clear();
            currentDialogQueue.Enqueue(nextDialog);
        }

        isShowingChoices = false;
        ShowNextLine();
    }

    private void EndDialog()
    {
        isDialogActive = false;
        dialogUI.SetActive(false);
        checkIsNeedLoadMiniGame();
       
    }

    private void checkIsNeedLoadMiniGame()
    {
        if (currentEventType == EventType.Game1)
        {
            littleGameParent.transform.position = new Vector3(transform.position.x, transform.position.y, -8);
            GameObject miniGameInstance = Instantiate(keyGamePrefab, littleGameParent.transform.position, Quaternion.identity);
            miniGameInstance.transform.SetParent(littleGameParent.transform, false);
            miniGameInstance.transform.localPosition = Vector3.zero;

        }
        else if (currentEventType == EventType.Game2)
        {
            PlayerManager.Instance.TogglePlayer(false);
            player.SetActive(false);
            littleGameParent.transform.position = transform.position;
            GameObject miniGameInstance = Instantiate(puzzleGamePrefab, littleGameParent.transform.position, Quaternion.identity);
            miniGameInstance.transform.SetParent(littleGameParent.transform, false);
            miniGameInstance.transform.localPosition = Vector3.zero;
        }
        else 
        {
             GetComponent<PlayerMoveScript>().canMove = true;
        }
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
