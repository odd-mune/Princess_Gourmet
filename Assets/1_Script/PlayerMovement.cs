using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum PlayerState
{
    walk,
    run,
    attack,
    interact
}


public class PlayerMovement : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;

    private float speed;
    private PlayerState currentState;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;

    void Start()
    {
        SetCurrentState(PlayerState.walk);
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
    }

    void Update()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw ("Horizontal");
        change.y = Input.GetAxisRaw ("Vertical"); 
        if(Input.GetButtonDown("attack") && currentState != PlayerState.attack)
        {   
            StartCoroutine(AttackCo());
        }
        else if(Input.GetButtonDown("run") && currentState == PlayerState.walk)
        {
            SetCurrentState(PlayerState.run);
        }
        else if(Input.GetButtonUp("run") && currentState == PlayerState.run)
        {
            SetCurrentState(PlayerState.walk);
        }

        if(currentState == PlayerState.walk || currentState == PlayerState.run)
        {
            UpdateAnimationAndMove();
        }
    }

    private IEnumerator AttackCo()
    {
        PlayerState prevState = currentState;
        animator.SetBool("attacking", true);
        SetCurrentState(PlayerState.attack);
        yield return null;
        animator.SetBool("attacking", false);
        yield return new WaitForSeconds(.3f);
        SetCurrentState(prevState);
    }

    void UpdateAnimationAndMove()
    {
        if(change != Vector3.zero)
        {
            MoveCharacter();
            animator.SetFloat("moveX", change.x);
            animator.SetFloat("moveY", change.y);
            animator.SetBool("moving", true);
        }
        else
        {
            animator.SetBool("moving", false);
        }
    }


    void MoveCharacter()
    {
        change.Normalize();
        myRigidbody.MovePosition
        (
            transform.position + change * speed * Time.deltaTime
        );
    }

    //run 기능을 만들기 위해 새로 만든
    private void SetCurrentState(PlayerState newState)
    {
        switch (newState)
        {
            case PlayerState.walk:
            speed = walkSpeed;
            break;
            case PlayerState.run:
            speed = runSpeed;
            break;
            case PlayerState.attack:
            speed = 0;
            break;
            case PlayerState.interact:
            speed = 0;
            break;
            default:
            Assert.IsTrue(false);
            break;
        }
        currentState = newState;
    }
}
