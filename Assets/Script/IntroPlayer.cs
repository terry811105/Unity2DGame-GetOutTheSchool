using UnityEngine;
using DG.Tweening;
using TMPro;

public class IntroPlayer : MonoBehaviour
{
    public TextAsset textFiled;
    public Transform character; // 角色的Transform组件
    public Vector3 targetPosition; // 目标位置
    public float moveDuration = 2f; // 移动持续时间
    Rigidbody2D rdBody;
    public GameObject dialogUI; // 對話框UI物件
    public TextMeshProUGUI textUI;
    string[] lineData;
    int displayIndex = 0;
    private bool dialogUIDidShow = false;
    bool isTextFinish = false;
    bool isAnimationFinish = false;
    Animator animator;
    public float speed = 5f;
    private TypewriterEffect typewriterEffect;
    void Start()
    {
        animator = GetComponent<Animator>();
        rdBody = GetComponent<Rigidbody2D>();
        dialogUI.SetActive(false);
        // 使用DOTween移动角色到目标位置
        character.DOMove(targetPosition, moveDuration).OnComplete(OnMoveComplete);

        GetTextFormFile(textFiled);

         typewriterEffect = textUI.GetComponent<TypewriterEffect>();
         if (typewriterEffect == null)
        {
            typewriterEffect = textUI.gameObject.AddComponent<TypewriterEffect>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        ControlDialog();
        // ControlMove();
    }

    void FixedUpdate()
    {
        ControlMove();
    }

    void ControlMove()
    {
        if (isAnimationFinish && isTextFinish) 
        {
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            if (horizontal > 0)
            {
                character.localScale = new Vector3(1.7f, 1.7f, 1.7f);
            } 
            else if (horizontal < 0)
            {
                character.localScale = new Vector3(-1.7f, 1.7f, 1.7f);
            }
            
            Vector2 movement = new Vector2(horizontal, vertical);

            rdBody.velocity = speed * movement;

            float speedValue = movement.magnitude;
            animator.SetFloat("Run", speedValue);
        }
        
    }

    void OnMoveComplete()
    {
        // 移动完成后的操作，例如开始对话
        Debug.Log("done");
        dialogUI.SetActive(true);
        dialogUIDidShow = true;
        isAnimationFinish = true;
        TypeText();
    }

    void GetTextFormFile(TextAsset file)
    {
        //將檔案文字分割並儲存成字串陣列
        lineData = file.text.Split('\n'); //換行就切割
        // if (lineData.Length > 0)
        // {
        //     textUI.text = lineData[0];
        //     displayIndex++;
        // }
    }

    void ControlShowText()
    {

        if (Input.GetKeyDown(KeyCode.Space) && displayIndex < lineData.Length && dialogUIDidShow)
        {
            TypeText();
        }

        if (displayIndex == lineData.Length)
        {
            isTextFinish = true;
        }
    }

    void TypeText() 
    {
        typewriterEffect.StartTyping(lineData[displayIndex]);
        displayIndex++;
    }

    void ControlDialog()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {


            if (dialogUIDidShow && isTextFinish == false)
            {
                ControlShowText();
            }

            else if (dialogUIDidShow && isTextFinish)
            {
                dialogUI.SetActive(false);
                dialogUIDidShow = false;
            }

        }
    }
}
