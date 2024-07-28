using UnityEngine;
using System.Collections;

public class KeyGameManager : MonoBehaviour
{
    public Transform fixedPoint;
    public MovingPoint movingPoint;
    public float threshold = 0.5f; // 臨界點
    public GameObject successCirclePrefab; // 新增：成功時顯示的圓圈預製體
    public float circleDisplayTime = 1f; // 新增：圓圈顯示的時間
    private bool isMoving = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Vector2.Distance(fixedPoint.position, movingPoint.transform.position) < threshold)
            {
                Debug.Log("成功！");
                // 在這裡添加成功的邏輯
                StartCoroutine(SuccessSequence());
            }
            else
            {
                Debug.Log("失敗！");
                // 在這裡添加失敗的邏輯
            }
        }
    }

    IEnumerator SuccessSequence()
    {
        isMoving = true;
        yield return StartCoroutine(ShowSuccessCircle());
        MoveFixedPointToRandomPosition();
        isMoving = false;
    }

    IEnumerator ShowSuccessCircle()
    {
        Vector3 circlePosition = movingPoint.transform.position;
        GameObject circle = Instantiate(successCirclePrefab, circlePosition, Quaternion.identity);
        // circle.transform.localScale = Vector3.one * threshold * 2;

        yield return new WaitForSeconds(circleDisplayTime);

        Destroy(circle);
    }

    void MoveFixedPointToRandomPosition()
    {
        float radius = movingPoint.radius;  // 使用 MovingPoint 的 radius
        
        // 生成一個隨機角度（0到360度）
        float randomAngle = Random.Range(0f, 360f);
        // 將角度轉換為弧度
        float randomRadian = randomAngle * Mathf.Deg2Rad;

        // 計算新的x和y坐標
        float x = Mathf.Cos(randomRadian) * radius;
        float y = Mathf.Sin(randomRadian) * radius;

        // 設置fixedPoint的新位置
        fixedPoint.position = new Vector3(x, y, 0);
    }

}
