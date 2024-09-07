using UnityEngine;
using System.Collections.Generic;

public class PuzzleGameManager : BasePrefab
{
    public GameObject puzzlePiecePrefab;
    public Transform puzzleArea;
    public Vector2Int puzzleSize = new Vector2Int(4, 3);
    public float pieceSpacing = 1.1f;

    private PuzzlePiece[,] puzzleGrid;
    private int placedPieces = 0;

    bool IsValidGridPosition(Vector2Int gridPos)
    {
        return gridPos.x >= 0 && gridPos.x < puzzleSize.x && gridPos.y >= 0 && gridPos.y < puzzleSize.y;
    }

    void Start()
    {
        puzzleGrid = new PuzzlePiece[puzzleSize.x, puzzleSize.y];
        // SetupPuzzlePieces();
    }

    

    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        Vector3 localPos = worldPosition - puzzleArea.position;
        int x = Mathf.RoundToInt(localPos.x / pieceSpacing);
        int y = Mathf.RoundToInt(localPos.y / pieceSpacing);
        return new Vector2Int(x, y);
    }

    public Vector3 GetWorldPosition(Vector2Int gridPosition)
    {
        return new Vector3(gridPosition.x * pieceSpacing, gridPosition.y * pieceSpacing, 0) + puzzleArea.position;
    }

    public void PlacePiece(PuzzlePiece piece, Vector2Int gridPosition)
    {
        if (IsValidGridPosition(gridPosition) && puzzleGrid[gridPosition.x, gridPosition.y] == null)
        {
            Debug.Log("拼對一個！");
            puzzleGrid[gridPosition.x, gridPosition.y] = piece;
            placedPieces++;
            CheckPuzzleCompletion();
        }
    }

    public void RemovePiece(PuzzlePiece piece, Vector2Int gridPosition)
    {
        Vector2Int gridPos = GetGridPosition(piece.transform.position);
        if (IsValidGridPosition(gridPos) && puzzleGrid[gridPos.x, gridPos.y] == piece)
        {
            puzzleGrid[gridPos.x, gridPos.y] = null;
            placedPieces--;
        }
    }

    void CheckPuzzleCompletion()
    {
        if (placedPieces == puzzleSize.x * puzzleSize.y)
        {
            PlayerManager.Instance.TogglePlayer(true);

            // GetComponent<PlayerMoveScript>().canMove = true;
            Debug.Log("拼圖完成！");
            // 在這裡添加完成拼圖後的操作
            DestroyEntirePrefab("PuzzleGame");
            PlayerMoveScript playerScript = FindObjectOfType<PlayerMoveScript>();
                if (playerScript != null)
                {
                    playerScript.canMove = true;
                }
        }
    }

    Vector3 GetRandomPosition()
    {
        float screenWidth = Screen.width;
        float screenHeight = Screen.height;
        Vector3 randomScreenPosition = new Vector3(Random.Range(0, screenWidth), Random.Range(0, screenHeight), 0);
        return Camera.main.ScreenToWorldPoint(randomScreenPosition);
    }
}
