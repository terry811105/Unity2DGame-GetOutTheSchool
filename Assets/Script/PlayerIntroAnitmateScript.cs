using UnityEngine;
using DG.Tweening;

public class PlayerIntroAnitmateScript : MonoBehaviour
{
    public Vector3 targetPosition; // 目标位置
    public float moveDuration = 2f; // 移动持续时间
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        MoveToStartPosition();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void MoveToStartPosition()
    {
        transform.DOMove(targetPosition, moveDuration).OnComplete(OnMoveComplete);
    }

    void OnMoveComplete()
    {
        // 通知其他腳本動畫完成
        GetComponent<PlayerDialogEventScript>().OnIntroAnimationComplete();
        
        transform.position = targetPosition;
    }
}
