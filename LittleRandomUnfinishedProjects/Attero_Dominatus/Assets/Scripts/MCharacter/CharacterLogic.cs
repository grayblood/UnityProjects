using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public enum CharacterEffectName { Normal, FastEffect }
public enum CharacterState { Idle_Down, Idle_Up, Idle_Right, Idle_Left, Walking_Down, Walking_Up, Walking_Right, Walking_Left }
public class CharacterLogic : MonoBehaviour
{
    [SerializeField]
    List<ScriptableEffect> effects = new List<ScriptableEffect>();

    public LayerMask solidObject;
    ScriptableEffect currentEffect;

    CharacterEffectName e_CEN;
    CharacterState e_State;

    //Player Move variables

    [SerializeField]
    float moveSpeed;
    bool isMoving;
    Vector2 input;

    Animator anmtr;

    private void Awake()
    {
        //currentEffect = effects[0];
        e_CEN = CharacterEffectName.Normal;
        e_State = CharacterState.Idle_Down;
        anmtr = GetComponent<Animator>();
    }
    private void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");

            //No diagonals
            if (input.x != 0) input.y = 0;

            if (input != Vector2.zero)
            {
                anmtr.SetFloat("moveX", input.x);
                anmtr.SetFloat("moveY", input.y);
                var targetPos = transform.position;
                targetPos.x += input.x;
                targetPos.y += input.y;

                if (IsWalkable(targetPos))
                    StartCoroutine(Move(targetPos));
            }
        }
        anmtr.SetBool("isMoving", isMoving);
    }
    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;
        isMoving = false;
    }
    bool IsWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(new Vector2(targetPos.x, targetPos.y + 0.25f), 0.1f, solidObject) != null)
        {
            return false;
        }
        return true;
    }
}
