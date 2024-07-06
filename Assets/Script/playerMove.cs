using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class playerMove : MonoBehaviour
{

    public GameObject player;
    private Rigidbody2D body;

    public GameObject promptUI; 
    public GameObject dialogUI; 
    public TextMeshProUGUI text;
    private bool playerInRange = false;
    private bool dialogUIDidShow = false;
    private bool isCanUp = true;
    private bool isCanDown = true;
    private bool isCanRight = true;
    private bool isCanLeft = true;
    private bool isUp = false;
    private bool isDown = false;
    private bool isRight = false;
    private bool isLeft = false;
   
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {

        move();

        
    }

    private void FixedUpdate()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        stopmoving();
        Debug.Log("OnCollisionEnter2D");
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        //body.velocity = new Vector2(0, 0);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // setCanMove();
        Debug.Log("OnCollisionExit2D");
    }

    void move()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            player.transform.position += new Vector3(0.01f, 0, 0);
            isRight = true;
        }
        else
        {
            isRight = false;
        }

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            player.transform.position += new Vector3(-0.01f, 0, 0);
        }

        if (Input.GetKey(KeyCode.UpArrow) && isCanUp)
        {
            player.transform.position += new Vector3(0, 0.01f, 0);
            isUp = true;
        }
        else
        {
            isUp = false;
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            player.transform.position += new Vector3(0, -0.01f, 0);
        }
    }

    void stopmoving()
    {
        if (isUp)
        {
            isCanUp = false;
        }

    }

    void setCanMove()
    {
        isCanUp = true;
    }
}
