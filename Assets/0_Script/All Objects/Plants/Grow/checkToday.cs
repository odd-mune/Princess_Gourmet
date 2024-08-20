using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class CheckToday : MonoBehaviour
{
    public static float elapsedSeconds;

    // Start is called before the first frame update
    void Start()
    {
    }
 
    // Update is called once per frame
    void FixedUpdate()
    {
        elapsedSeconds += Time.fixedDeltaTime;
    }
}