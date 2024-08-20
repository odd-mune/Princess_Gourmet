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

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        // 플레이어와 충돌 방지 
        Physics2D.IgnoreLayerCollision(6, 7);
        currentSpeed = speed;
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, currentSpeed * Time.fixedDeltaTime);
        }


        // 플레이어와 일정 거리 이상 떨어지면 자동 텔레포트 기능 
        float distanceBetweenTarget = Vector2.Distance(target.position, transform.position);
        if (distanceBetweenTarget > teleportDistance)
        {
            Vector3 direction = Vector3.Normalize(transform.position - target.position);
            transform.position = target.position + direction * distance;
        //     teleportEffect.gameObject.SetActive(true);
        //     teleportEffect.Play();
        }
        
        // if (!teleportEffect.isPlaying)
        // {
        //     teleportEffect.gameObject.SetActive(false);
        // }
    }
}
