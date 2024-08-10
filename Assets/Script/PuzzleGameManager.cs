using UnityEngine;
using System.Collections.Generic;

public class PuzzleGameManager : MonoBehaviour
{
   public Vector2Int puzzleSize = new Vector2Int(4, 4);
    private PuzzlePiece[,] puzzleGrid;
    public Transform puzzleArea;
    public float pieceSpacing = 1.1f;
    private List<PuzzlePiece> puzzlePieces = new List<PuzzlePiece>();
    private int placedPieces = 0;
    
    void Start()
    {
        puzzleGrid = new PuzzlePiece[puzzleSize.x, puzzleSize.y];
        SetupPuzzlePieces();
    }

    void SetupPuzzlePieces()
    {
        PuzzlePiece[] pieces = FindObjectsOfType<PuzzlePiece>();
        foreach (PuzzlePiece piece in pieces)
        {
            // 根據拼圖塊的初始位置設置correctGridPosition
            piece.correctGridPosition = GetGridPosition(piece.transform.position);
        }
    }

    Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        // 將世界坐標轉換為網格坐標
        // 這裡需要根據你的場景設置進行調整
        int x = Mathf.RoundToInt(worldPosition.x / pieceSpacing);
        int y = Mathf.RoundToInt(worldPosition.y / pieceSpacing);
        return new Vector2Int(x, y);
    }

    public Vector3 GetWorldPosition(Vector2Int gridPosition)
{
    // 將網格坐標轉換為世界坐標
    return new Vector3(gridPosition.x * pieceSpacing, gridPosition.y * pieceSpacing, 0) + puzzleArea.position;
}

public void PlacePiece(PuzzlePiece piece, Vector2Int gridPosition)
{
    if (puzzleGrid[gridPosition.x, gridPosition.y] == null)
    {
        puzzleGrid[gridPosition.x, gridPosition.y] = piece;
        placedPieces++;
        CheckPuzzleCompletion();
    }
}

public void RemovePiece(PuzzlePiece piece)
{
    Vector2Int gridPos = GetGridPosition(piece.transform.position);
    if (puzzleGrid[gridPos.x, gridPos.y] == piece)
    {
        puzzleGrid[gridPos.x, gridPos.y] = null;
        placedPieces--;
    }
}

void CheckPuzzleCompletion()
{
    if (placedPieces == puzzleSize.x * puzzleSize.y)
    {
        Debug.Log("拼圖完成！");
        // 在這裡添加完成拼圖後的操作
    }
}
}
