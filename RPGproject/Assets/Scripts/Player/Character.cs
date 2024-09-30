using System;
using System.Collections;
using UnityEngine;

public class Character : MonoBehaviour
{
    private Animator animator;
    public float moveSpeed;

    public bool IsMoving { get; set; }
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    public IEnumerator Move(Vector2 moveVec, Action OnMoveOver=null)
    {
        animator.SetFloat("moveX", moveVec.x);
        animator.SetFloat("moveY", moveVec.y);

        var targetPos = transform.position;
        targetPos.x += moveVec.x;
        targetPos.y += moveVec.y;
        if (!IsWalkable(targetPos))
            yield break;

        IsMoving = true;

        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        IsMoving = false;
        OnMoveOver?.Invoke();


    }
    public void HandleUpdate()
    {
        animator.SetBool("isMoving", IsMoving);
    }
  
    private bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.3f, GameLayers.i.SolidLayer | GameLayers.i.InteractableLayer ) != null)
        {
            return false;
        }

        return true;
    }
    public Animator Animator
    {
        get => animator;
    }
}
