using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pet : MonoBehaviour
{
    public float speed;
    public float distance;
    Transform player;
    Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        Physics2D.IgnoreLayerCollision(6, 7);
    }

    void Update()
    {
        // 가로
        if (Mathf.Abs(transform.position.x - player.position.x) > distance)
        {
            transform.Translate(new Vector2(-1, 0) * Time.deltaTime * speed);
            anim.SetBool("IsWalk", true);
            DirectionPet();
        }
        else
        {
            anim.SetBool("IsWalk", false);
        }

        // 세로 
        if (Mathf.Abs(transform.position.y - player.position.y) > distance)
        {
            transform.Translate(new Vector2(0, -1) * Time.deltaTime * speed);
            anim.SetBool("IsWalk", true);
            DirectionPet();
        }
        else
        {
            anim.SetBool("IsWalk", false);
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

    void DirectionPet()
    {
        if (transform.position.x - player.position.x < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }

        // 세로
        if (transform.position.y - player.position.y < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
        }
        else
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }
}
