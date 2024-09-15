using UnityEngine;

public class MovingPoint : MonoBehaviour
{
    public float speed = 5f;
    public float radius = 4f;

    private float angle = 0f;
    private Vector3 centerPoint;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
         centerPoint = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move() 
    {
        angle += speed * Time.deltaTime;
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;
        transform.localPosition = new Vector3(x, y, 3.4f);
    }
}
