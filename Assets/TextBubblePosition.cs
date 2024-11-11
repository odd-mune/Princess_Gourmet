using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextBubblePosition : MonoBehaviour
{
    public Transform player;
    public Vector3 offset;

    void Update() 
    {
        transform.localPosition = player.position + offset;
    }
}
