using UnityEngine;

public class KeyGameManager : MonoBehaviour
{
    public Transform fixedPoint;
    public Transform movingPoint;
    public float threshold = 0.5f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (Vector2.Distance(fixedPoint.position, movingPoint.position) < threshold)
            {
                Debug.Log("成功！");
                // 在這裡添加成功的邏輯
            }
            else
            {
                Debug.Log("失敗！");
                // 在這裡添加失敗的邏輯
            }
        }
    }
}
