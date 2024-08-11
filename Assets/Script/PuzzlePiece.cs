using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    public Vector2 correctGridPosition;
    private Vector3 dragOffset;
    private Camera cam;
    private PuzzleGameManager gameManager;
    private bool isDragging = false;

    void Start()
    {
        cam = Camera.main;
        gameManager = FindAnyObjectByType<PuzzleGameManager>();
    }

    void OnMouseDown()
    {
        dragOffset = transform.position - GetMousePosition();
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            transform.position = GetMousePosition() + dragOffset;
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        Vector2Int currentGridPos = gameManager.GetGridPosition(transform.position);
        
        if (currentGridPos == correctGridPosition)
        {
            transform.position = gameManager.GetWorldPosition(currentGridPos);
            gameManager.PlacePiece(this, currentGridPos);
        }
        else
        {
            gameManager.RemovePiece(this);
        }
    }

    Vector3 GetMousePosition()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        return mousePos;
    }

}
