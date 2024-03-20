using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAction : MonoBehaviour
{
    public float Speed;

    Rigidbody2D rigid;
    float h;
    float v;

    void Awake()
    {   
        rigid = GetComponent<Rigidbody2D>();        
    }

    void Update()
    {
        h = Input.GetAxisRaw ("Horizontal");
        v = Input.GetAxisRaw ("Vertical");   
    }
            

    void FixedUpdate()
    {
        rigid.velocity = new Vector2(h,v) * Speed;
    }

}