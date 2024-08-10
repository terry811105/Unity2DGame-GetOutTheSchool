using UnityEngine;

public class PuzzlePiece : MonoBehaviour
{
    public Vector2Int correctGridPosition; // 正確的網格位置
    public float snapThreshold = 0.5f; // 吸附閾值
    public Vector2 correctPosition;
    private Vector3 offset;
    private bool isDragging = false;
    private PuzzleGameManager gameManager;

    [System.Obsolete]
    void Start()
    {
        gameManager = FindObjectOfType<PuzzleGameManager>();
    }

    void OnMouseDown()
    {
        offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        isDragging = true;
    }

    void OnMouseDrag()
    {
        if (isDragging)
        {
            Vector3 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
            transform.position = new Vector3(newPosition.x, newPosition.y, 0);
        }
    }

    void OnMouseUp()
    {
        isDragging = false;
        Vector2Int currentGridPos = gameManager.GetGridPosition(transform.position);

        if (currentGridPos == correctGridPosition)
        {
            // 吸附到正確位置
            transform.position = gameManager.GetWorldPosition(currentGridPos);
            gameManager.PlacePiece(this, currentGridPos);
        }
        else
        {
            gameManager.RemovePiece(this);
        }
    }
}
