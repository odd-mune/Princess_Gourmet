using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour
{
    public float speed;
    private float currentSpeed;
    public float distance;
    private Transform target;
    public float teleportDistance;
    //public ParticleSystem teleportEffect;

    private Animator animator;
    private Vector2 mDefaultScale;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        // 플레이어와 충돌 방지 
        Physics2D.IgnoreLayerCollision(6, 7);
        currentSpeed = speed;

        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.LogError("Pet에 Animator component가 없습니다!!");
            Debug.Break();
        }

        mDefaultScale = transform.localScale;
    }

    private void Update()
    {
        Vector3 direction = target.position - transform.position;
        Vector3 normalizedDirection = direction.normalized;
        animator.SetFloat("moveX", normalizedDirection.x);
        animator.SetFloat("moveY", normalizedDirection.y);

        if (normalizedDirection.x < 0)
        {
            transform.localScale = new Vector2(mDefaultScale.x, mDefaultScale.y);
        }
        else
        {
            transform.localScale = new Vector2(-mDefaultScale.x, mDefaultScale.y);
        }

        float distanceBetweenTarget = direction.magnitude;
        // 플레이어와 일정 거리 이상 떨어지면 자동 텔레포트 기능
        if (distanceBetweenTarget > teleportDistance)
        {
            transform.position = target.position + -normalizedDirection * distance;
            //     teleportEffect.gameObject.SetActive(true);
            //     teleportEffect.Play();
        }
        else if (distanceBetweenTarget > distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, currentSpeed * Time.fixedDeltaTime);
            animator.SetBool("isMoving", true);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

        // if (!teleportEffect.isPlaying)
        // {
        //     teleportEffect.gameObject.SetActive(false);
        // }
    }
}
