using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour
{
    public float speed;
    public float distance;
    private Transform target;

    private void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        // 플레이어와 충돌 방지 
        Physics2D.IgnoreLayerCollision(6, 7);
    }

    private void Update()
    {
        if (Vector2.Distance(transform.position, target.position) > distance)
        {
            transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }

        // 플레이어와 일정 거리 이상 떨어지면 자동 텔레포트 기능 
        // if (Vector2.Distance(player.position, transform.position) > teldistance)
        // {
        //     transform.position = player.position;
        //     tel.gameObject.SetActive(true);
        //     tel.Play();
        // }
        // if (!tel.isPlaying)
        // {
        //     tel.gameObject.SetActive(false);
        // }
    }
    //public ParticleSystem tel;
}
