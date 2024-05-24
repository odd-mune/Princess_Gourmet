using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public enum PlayerState
{
    walk,
    run,
    attack,
    interact,
    stagger,
    idle
}


public class PlayerManager : MonoBehaviour
{
    public float walkSpeed;
    public float runSpeed;

    private float speed;
    public PlayerState currentState;
    private Rigidbody2D myRigidbody;
    private Vector3 change;
    private Animator animator;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;
    public VectorValue startingPosition;

    private List<GameObject> mCurrentCollidingItems;
    private List<GameObject> mCurrentPickUpObjects;
    private bool hasConsumedSpaceKey;

    void Start()
    {
        // Limit the framerate to 30
        Application.targetFrameRate = 30;

        SetCurrentState(PlayerState.walk);
        animator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", -1);
        transform.position = startingPosition.initialValue;
        mCurrentCollidingItems = new List<GameObject>();
        mCurrentPickUpObjects = new List<GameObject>();
        hasConsumedSpaceKey = false;
    }

    void FixedUpdate()
    {
        change = Vector3.zero;
        change.x = Input.GetAxisRaw ("Horizontal");
        change.y = Input.GetAxisRaw ("Vertical"); 

        //당근 줍기
        if (mCurrentCollidingItems.Count > 0 && !hasConsumedSpaceKey && Input.GetKeyDown(KeyCode.Space))
        {
            GameObject itemGameObjectToPickUp = mCurrentCollidingItems[0];
            itemGameObjectToPickUp.GetComponent<PhysicalInventoryItem>().PickUp();
            
            mCurrentCollidingItems.RemoveAt(0);
            Destroy(itemGameObjectToPickUp);
            hasConsumedSpaceKey = true;
        }
        //시럽나무 줍기
        if (mCurrentPickUpObjects.Count > 0 && !hasConsumedSpaceKey && Input.GetKeyDown(KeyCode.Space))
        {
            GameObject itemGameObjectToPickUp = mCurrentPickUpObjects[0];
            itemGameObjectToPickUp.GetComponent<PhysicalInventoryItem>().PickUp();
            
            hasConsumedSpaceKey = true;
        }

        if (hasConsumedSpaceKey && Input.GetKeyDown(KeyCode.Space) == false)
        {
            hasConsumedSpaceKey = false;
        }

        if(Input.GetButtonDown("attack") && currentState != PlayerState.attack
            && currentState != PlayerState.stagger)
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
        if(currentState == PlayerState.walk || currentState == PlayerState.run
            || currentState == PlayerState.idle)
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

    //당근 수집 
    private void OnTriggerEnter2D(Collider2D other)
    {
        Collider2D collider = GetComponent<Collider2D>();
        if(collider.isTrigger == false && other.gameObject.CompareTag("item"))
        {
            if (mCurrentCollidingItems.Contains(other.gameObject) == false)
            {
                mCurrentCollidingItems.Add(other.gameObject);
            }

            if (mCurrentCollidingItems.Count == 1)
            {
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.9f);
            }
        }

        //시럽 수집 
        else if(collider.isTrigger == false && other.gameObject.CompareTag("PickUp Object"))
        {
            if (mCurrentPickUpObjects.Contains(other.gameObject) == false)
            {
                mCurrentPickUpObjects.Add(other.gameObject);
            }

            if (mCurrentPickUpObjects.Count == 1)
            {
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 0.9f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        //당근 수집 
        if (other.gameObject.CompareTag("item"))
        {
            mCurrentCollidingItems.Remove(other.gameObject);
            //Debug.Log($"Colliding with {mCurrentCollidingItems.Count} items");

            if (mCurrentCollidingItems.Count == 0)
            {
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }

        //시럽 나무
        else if (other.gameObject.CompareTag("PickUp Object"))
        {
            mCurrentPickUpObjects.Remove(other.gameObject);
            //Debug.Log($"Colliding with {mCurrentPickUpObjects.Count} items");

            if (mCurrentPickUpObjects.Count == 0)
            {
                GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);
            }
        }
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
            transform.position + change * speed * Time.fixedDeltaTime
        );
    }

    public void Knock(float knockTime, float damage)
    {
        currentHealth.RuntimeValue -= damage;
        playerHealthSignal.Raise();
        if(currentHealth.RuntimeValue > 0)
        {
            StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator KnockCo(float knockTime)
    {
        if(myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            currentState = PlayerState.walk;
                //넉백을 받으면 공주가 자꾸 가만히 멈춰서서 idle 에서 walk로 고쳐봤음
            myRigidbody.velocity = Vector2.zero;
        }
    }
  
    //run 기능을 만들기 위해 새로 만든
    private PlayerState SetCurrentState(PlayerState newState)
    //private void SetCurrentState(PlayerState newState)
    //넉백 스크립트에서 자꾸 player.currentstate 프로텍션 레벨 때문에 접근을 못한다고 해서
    //위에 playerstate 선언한거 퍼블릭으로 바꾸고 밑에 리턴을 주니까 해결했음. 맞나??
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
            //상태 두개 stagger, idle 새롭게 추가했음..위에도 추가했기 때문에 
            case PlayerState.stagger:
            speed = 0;
            break;
            case PlayerState.idle:
            speed = 0;
            break;
            default:
            Assert.IsTrue(false);
            break;
        }
        currentState = newState;
        return currentState;
    }
}