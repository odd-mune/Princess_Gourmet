using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class GrowUp_raspberryBush : MonoBehaviour
{
    int today;
    GameObject raspberryBush_no;
    GameObject raspberryBush_yes;
 
    // Start is called before the first frame update
    void Start()
    {
        today = 0;
        raspberryBush_no = transform.GetChild(0).gameObject;
        raspberryBush_yes = transform.GetChild(1).gameObject;
    }
 
    // Update is called once per frame
    void Update()
    {
        today = CheckToday.Today;
        growing_up(today);
     
    }
 
    void growing_up(int today)
    {
        if (today < 5) 
        {
            if (raspberryBush_no.activeSelf == false)
                raspberryBush_no.SetActive(true);
        }
        else if (today >= 5) 
        {
            if (raspberryBush_no.activeSelf == true)
                raspberryBush_no.SetActive(false);
            if (raspberryBush_yes.activeSelf == false)
                raspberryBush_yes.SetActive(true);
        }
    }
}