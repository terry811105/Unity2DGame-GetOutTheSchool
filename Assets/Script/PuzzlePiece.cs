using UnityEngine;
using UnityEngine.EventSystems;

public class PuzzlePiece : MonoBehaviour, IPointerClickHandler
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
        Debug.Log($"Piece position: {transform.position}, Is visible: {GetComponent<Renderer>().isVisible}");
        if (GetComponent<Collider2D>() == null)
        {
            Debug.LogError($"No Collider2D on {gameObject.name}");
            gameObject.AddComponent<BoxCollider2D>();
        }

        var renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Debug.Log($"Renderer enabled: {renderer.enabled}, Object is visible: {renderer.isVisible}");
        }
        else
        {
            Debug.LogWarning("No Renderer found on this object.");
        }

        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            Vector3 viewPos = mainCamera.WorldToViewportPoint(transform.position);
            Debug.Log($"Viewport Position: {viewPos}");

            bool isInCameraView = viewPos.z >= mainCamera.nearClipPlane && viewPos.z <= mainCamera.farClipPlane;
            if (!isInCameraView)
            {
                Debug.Log($"Object {gameObject.name} is outside the camera's clipping planes.");
            }
            else
            {
                Debug.Log($"Object {gameObject.name} is inside the camera's clipping planes.");
            }
        }

        if ((mainCamera.cullingMask & (1 << gameObject.layer)) == 0)
        {
            Debug.Log($"Object {gameObject.name} is not in the camera's culling mask.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // 按空格鍵測試
        {
            OnMouseDown();
        }

        checkClick();
        checkVisible();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Pointer clicked on this object");
    }

    void checkClick()
    {
        if (Input.GetMouseButtonDown(0)) // 左鍵點擊
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Collider2D hitCollider = Physics2D.OverlapPoint(mousePos);
            if (hitCollider != null)
            {
                Debug.Log($"OverlapPoint hit: {hitCollider.gameObject.name}");
            }
            else
            {
                Debug.Log("OverlapPoint didn't hit anything");
            }
        }
    }

    void checkVisible()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray);

            if (hit.collider != null)
            {
                Debug.Log($"Raycast hit: {hit.collider.gameObject.name}");
            }
            else
            {
                Debug.Log("Raycast didn't hit anything");
            }
        }
    }

    void OnMouseDown()
    {
        Debug.Log("OnMouseDown");
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
        Vector2 currentPosition = transform.localPosition;
        float distance = Vector2.Distance(currentPosition, correctPosition);
        Debug.Log("distance: " + distance + ", current: " + currentPosition + ", correct: " + correctPosition);
        if (distance < fitRange)
        {
            transform.localPosition = correctPosition;
            gameManager.PlacePiece(this, correctGridPosition);
        }
        else
        {
            gameManager.RemovePiece(this);
        }
    }

    Vector3 GetMousePosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -cam.transform.position.z;
        return cam.ScreenToWorldPoint(mousePos);
    }

}
