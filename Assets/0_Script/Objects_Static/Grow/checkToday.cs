using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class CheckToday : MonoBehaviour
{
    public static int Today;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Today: " + Today);
    }
 
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Today += 1;
            KeyDown_Space();
        }
    }
    private void KeyDown_Space()
    {
        Debug.Log("Today: " + Today);
    }
}