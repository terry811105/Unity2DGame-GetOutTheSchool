using System.Diagnostics.Tracing;
using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour
{
    public Vector2 correctPosition;

    public Vector2Int correctGridPosition;
    private Vector3 dragOffset;
    private Camera cam;
    private PuzzleGameManager gameManager;
    private bool isDragging = false;

    float fitRange = 0.1f;

    void Start()
    {
        cam = Camera.main;
        gameManager = FindAnyObjectByType<PuzzleGameManager>();
        
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && IsMouseOverPuzzlePiece())
        {
            OnStartDrag();
        }
        else if (Input.GetMouseButton(0) && isDragging)
        {
            OnDrag();
        }
        else if (Input.GetMouseButtonUp(0) && isDragging)
        {
            OnEndDrag();
        }
        else if (Input.GetKey(KeyCode.K))
        {
            transform.localPosition = correctPosition;
            gameManager.PlacePiece(this, correctGridPosition);
        }
    }

    private void OnStartDrag()
    {
        dragOffset = transform.localPosition - GetMousePositionInWorld();
        isDragging = true;
    }

    private void OnDrag()
    {
        Vector3 newPosition = GetMousePositionInWorld() + dragOffset;
        transform.localPosition = newPosition;
    }

    private void OnEndDrag()
    {
        isDragging = false;
        Vector2 currentPosition = transform.localPosition;
        float distance = Vector2.Distance(currentPosition, correctPosition);
        Debug.Log($"Distance: {distance}, Current: {currentPosition}, Correct: {correctPosition}");

        if (distance < fitRange)
        {
            transform.localPosition = correctPosition;
            gameManager.PlacePiece(this, correctGridPosition);
        }
        else
        {
            gameManager.RemovePiece(this, correctGridPosition);
        }
    }

    private Vector3 GetMousePositionInWorld()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -cam.transform.position.z;
        return cam.ScreenToWorldPoint(mousePos);
    }

    private bool IsMouseOverPuzzlePiece()
    {
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);
        return hit.collider != null && hit.collider.gameObject == gameObject;
    }

    public void SetCorrectPosition(Vector2 position, Vector2Int gridPosition)
    {
        correctPosition = position;
        correctGridPosition = gridPosition;
    }

    // void OnMouseDown()
    // {
    //     Debug.Log("OnMouseDown");
    //     dragOffset = transform.localPosition - GetMousePosition();
    //     isDragging = true;
    // }

    // void OnMouseDrag()
    // {
    //     if (isDragging)
    //     {
    //         transform.localPosition = transform.parent.InverseTransformPoint(GetMousePosition() + dragOffset);
    //     }
    // }

    // void OnMouseUp()
    // {
    //     isDragging = false;
    //     Vector2 currentPosition = transform.localPosition;
    //     float distance = Vector2.Distance(currentPosition, correctPosition);
    //     Debug.Log("distance: " + distance + ", current: " + currentPosition + ", correct: " + correctPosition);
    //     if (distance < fitRange)
    //     {
    //         transform.localPosition = correctPosition;
    //         gameManager.PlacePiece(this, correctGridPosition);
    //     }
    //     else
    //     {
    //         gameManager.RemovePiece(this);
    //     }
    // }

    // Vector3 GetMousePosition()
    // {
    //     Vector3 mousePos = Input.mousePosition;
    //     mousePos.z = -Camera.main.transform.position.z;
    //     return Camera.main.ScreenToWorldPoint(mousePos);
    // }

}
