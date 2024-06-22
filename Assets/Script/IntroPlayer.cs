using UnityEngine;
using DG.Tweening;
using TMPro;

public class IntroPlayer : MonoBehaviour
{
    public TextAsset textFiled;
    public Transform character; // 角色的Transform组件
    public Vector3 targetPosition; // 目标位置
    public float moveDuration = 2f; // 移动持续时间

    public GameObject dialogUI; // 對話框UI物件
    public TextMeshProUGUI textUI;
    string[] lineData;
    int displayIndex = 0;
    private bool dialogUIDidShow = false;
    bool isTextFinish = false;

    void Start()
    {
        dialogUI.SetActive(false);
        // 使用DOTween移动角色到目标位置
        character.DOMove(targetPosition, moveDuration).OnComplete(OnMoveComplete);

        GetTextFormFile(textFiled);
    }

    // Update is called once per frame
    void Update()
    {
        controlDialog();
    }

    void OnMoveComplete()
    {
        // 移动完成后的操作，例如开始对话
        Debug.Log("done");
        dialogUI.SetActive(true);
        dialogUIDidShow = true;
    }

    void GetTextFormFile(TextAsset file)
    {
        //將檔案文字分割並儲存成字串陣列
        lineData = file.text.Split('\n'); //換行就切割
        if (lineData.Length > 0)
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
            displayIndex++;

        }

        if (displayIndex == lineData.Length)
        {
            isTextFinish = true;
        }
    }

    void controlDialog()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {


            if (dialogUIDidShow && isTextFinish == false)
            {
                showText();
            }

            else if (dialogUIDidShow && isTextFinish)
            {
                dialogUI.SetActive(false);
                dialogUIDidShow = false;
            }

        }
    }
}
