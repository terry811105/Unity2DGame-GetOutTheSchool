using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class playerMove : MonoBehaviour
{

    public GameObject player;
    private Rigidbody2D body;

    public GameObject promptUI; // 顯示提示的UI物件
    public GameObject dialogUI; // 對話框UI物件
    public TextMeshProUGUI text;
    private bool playerInRange = false;
    private bool dialogUIDidShow = false;
    private bool isCanUp = false;
    private bool isCanDown = false;
    private bool isCanRight = false;
    private bool isCanLeft = false;
    private bool isUp = false;
    private bool isDown = false;
    private bool isRight = false;
    private bool isLeft = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.Space))
        {
            if (dialogUIDidShow == false)
            {
                text.text = StaticData.spaceDialog;
                // 在玩家靠近且按下空白鍵時觸發對話框
                dialogUI.SetActive(true);
                dialogUIDidShow = true;
            }
            else
            {
                dialogUI.SetActive(false);
                dialogUIDidShow = false;
            }

        }
    }

    private void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            gameObject.transform.position += new Vector3(0.1f, 0, 0);
            isRight = true;
        } else
        {
            isRight = false;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            gameObject.transform.position += new Vector3(-0.1f, 0, 0);
        }

        if (Input.GetKey(KeyCode.UpArrow) && isCanUp)
        {
            gameObject.transform.position += new Vector3(0, 0.1f, 0);
            isUp = true;
        } else
        {
            isUp = false;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            gameObject.transform.position += new Vector3(0, -0.1f, 0);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        stopmoving();

        //body.velocity = new Vector2(0, 0);
       // body.constraints = RigidbodyConstraints2D.FreezeAll;
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //body.velocity = new Vector2(0, 0);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
       // body.velocity = new Vector2(0, 0);
    }

    void stopmoving()
    {
        if (isUp)
        {
            isCanUp = true;
        }
    }
}
